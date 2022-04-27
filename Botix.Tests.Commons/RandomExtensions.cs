using System;
using System.Collections.Generic;

namespace Botix.Tests.Commons
{
    internal static class RandomExtensions
    {
        public static TType Next<TType>(this Random random, IList<TType> collection) =>
            collection[random.Next(0, collection.Count)];
    }
}
