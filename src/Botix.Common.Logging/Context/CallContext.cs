using System.Threading;

namespace Botix.Common.Logging.Context
{
    /// <summary>
    ///     Call context used to store current request state.
    /// </summary>
    public static class CallContext
    {
        private static AsyncLocal<ContextProperties> AsyncState { get; } =
            new AsyncLocal<ContextProperties>();

        /// <summary>
        ///     Current request Call context state.
        /// </summary>
        public static ContextProperties State
        {
            get => AsyncState.Value;
            set => AsyncState.Value = value;
        }
    }
}
