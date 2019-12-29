using System;
using System.Collections.Generic;
using System.Linq;
using AlphaDev.Paging.Support;

namespace AlphaDev.Paging.Extensions
{
    public static class UInt32Extensions
    {
        public static uint MaxOf(this uint val1, uint val2) => Math.Max(val1, val2);

        public static uint MinOf(this uint val1, uint val2) => Math.Min(val1, val2);

        public static IEnumerable<uint> RangeTo(this uint start, uint end) => EnumerableSupport.RangeTo(start, end);

        public static IEnumerable<uint> RangeFrom(this uint end, uint start) => EnumerableSupport.RangeTo(start, end);
    }
}