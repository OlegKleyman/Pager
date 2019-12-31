using System;
using System.Collections.Generic;
using AlphaDev.Paging.Support;

namespace AlphaDev.Paging.Extensions
{
    public static class Int32Extensions
    {
        public static int MaxOf(this int val1, int val2) => Math.Max(val1, val2);

        public static int MinOf(this int val1, int val2) => Math.Min(val1, val2);

        public static IEnumerable<int> RangeTo(this int start, int end) => EnumerableSupport.RangeTo(start, end);

        public static IEnumerable<int> RangeFrom(this int end, int start) => EnumerableSupport.RangeTo(start, end);
    }
}