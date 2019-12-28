using System;
using System.Collections.Generic;
using System.Linq;
using AlphaDev.Paging.Extensions;
using AlphaDev.Paging.Support;
using Optional;

namespace AlphaDev.Paging
{
    public class Pages
    {
        private Pages(uint currentPage, uint lastPage)
        {
            First = 1;
            Current = currentPage;
            Last = lastPage;
            NextPage = Current.SomeWhen(u => u != lastPage).Map(u => u + 1);
            PreviousPages = 1u.RangeTo(1u.MaxOf(Current - 1)).ToArray();
            NextPages = lastPage.RangeFrom(lastPage.MinOf(Current + 1)).ToArray();
        }

        public uint[] NextPages { get; }

        public uint[] PreviousPages { get; }

        public uint Current { get; }

        public static Pages Create(uint currentPage, uint lastPage)
        {
            if (currentPage == 0) throw new ArgumentException($"Invalid value: {currentPage}", nameof(currentPage));

            if (lastPage < currentPage)
            {
                throw new ArgumentsException(
                    $"Invalid last page: {lastPage} It is less than than the current page: {currentPage}",
                    nameof(lastPage), nameof(currentPage));
            }

            return new Pages(currentPage, lastPage);
        }

        public uint First { get; }

        public uint Last { get; }

        public Option<uint> NextPage { get; }
    }
}