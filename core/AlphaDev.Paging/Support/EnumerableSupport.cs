using System.Collections.Generic;

namespace AlphaDev.Paging.Support
{
    public static class EnumerableSupport
    {
        public static IEnumerable<int> RangeTo(int start, int end)
        {
            if (start > end) throw new ArgumentsException("Start is greater than end.", nameof(start), nameof(end));

            for (var i = start; i <= end; i++)
            {
                yield return i;
            }
        }
    }
}