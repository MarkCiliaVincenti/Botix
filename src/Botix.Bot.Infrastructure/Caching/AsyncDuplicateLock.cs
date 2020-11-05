using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Botix.Bot.Infrastructure.Caching
{
    /// <summary>
    /// Async-compatible mutual exclusion mechanism
    /// 
    /// /// Using:
    /// 
    /// private readonly AsyncDuplicateLock locker = new AsyncDuplicateLock();
    /// public async Task UseLockAsync(object obj)
    /// {
    ///   /* AsyncDuplicateLock can be locked asynchronously */
    ///   using (await locker.LockAsync(obj))
    ///   {
    ///     /* It's safe to await while the lock is held */
    ///     await Task.Delay(TimeSpan.FromSeconds(1));
    ///   }
    /// }
    /// </summary>
    public sealed class AsyncDuplicateLock
    {
        private static readonly Dictionary<object, RefCounted<SemaphoreSlim>> SemaphoreSlims =
            new Dictionary<object, RefCounted<SemaphoreSlim>>();

        private SemaphoreSlim GetOrCreate(object key)
        {
            RefCounted<SemaphoreSlim> refCounted;
            lock (SemaphoreSlims)
            {
                if (SemaphoreSlims.TryGetValue(key, out refCounted))
                {
                    ++refCounted.RefCount;
                }
                else
                {
                    refCounted = new RefCounted<SemaphoreSlim>(new SemaphoreSlim(1, 1));
                    SemaphoreSlims[key] = refCounted;
                }
            }

            return refCounted.Value;
        }

        /// <summary>Synchronous lock</summary>
        /// <param name="key">Object to lock on</param>
        public IDisposable Lock(object key)
        {
            GetOrCreate(key).Wait();
            return new Releaser
            {
                Key = key
            };
        }

        /// <summary>Asynchronous lock</summary>
        /// <param name="key">Object to lock on</param>
        public async Task<IDisposable> LockAsync(object key)
        {
            await GetOrCreate(key).WaitAsync().ConfigureAwait(false);
            return new Releaser
            {
                Key = key
            };
        }

        private sealed class RefCounted<T>
        {
            public RefCounted(T value)
            {
                RefCount = 1;
                Value = value;
            }

            public int RefCount { get; set; }

            public T Value { get; }
        }

        private sealed class Releaser : IDisposable
        {
            public object Key { get; set; }

            public void Dispose()
            {
                RefCounted<SemaphoreSlim> semaphoreSlim;
                lock (SemaphoreSlims)
                {
                    semaphoreSlim = SemaphoreSlims[Key];
                    --semaphoreSlim.RefCount;
                    if (semaphoreSlim.RefCount == 0)
                        SemaphoreSlims.Remove(Key);
                }

                semaphoreSlim.Value.Release();
            }
        }
    }
}
