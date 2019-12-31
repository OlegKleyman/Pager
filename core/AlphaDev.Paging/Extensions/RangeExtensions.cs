using System;
using System.Collections.Generic;
using AlphaDev.Paging.Support;

namespace AlphaDev.Paging.Extensions
{
    public static class RangeExtensions
    {
        public static IEnumerable<int> ToEnumerable(this in Range range) =>
            EnumerableSupport.RangeTo(range.Start.Value, range.End.Value);
    }
}