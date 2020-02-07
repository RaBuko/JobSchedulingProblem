using System;
using System.Collections.Generic;
using System.Linq;

namespace Solver.Utils
{
    static class ICollectionExtension
    {
        public static IEnumerable<T> Clone<T>(this IEnumerable<T> enumerableToClone) where T : ICloneable
        {
            return enumerableToClone.Select(item => (T)item.Clone());
        }
    }
}
