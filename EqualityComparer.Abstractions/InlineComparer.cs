using System;
using System.Collections.Generic;

namespace EqualityComparer.Abstractions
{
    public class InlineComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> getEquals;
        private readonly Func<T, int> getHashCode;

        public InlineComparer(Func<T, T, bool> equals, Func<T, int> hashCode)
        {
            getEquals = equals;
            getHashCode = hashCode;
        }

        public bool Equals(T x, T y)
        {
            return getEquals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return getHashCode(obj);
        }
    }
}
