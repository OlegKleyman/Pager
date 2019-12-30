using System;
using System.Linq;
using AlphaDev.Paging.Extensions;
using Optional;
using Optional.Collections;

namespace AlphaDev.Paging
{
    public class Pages
    {
        private Pages(int currentPage, int lastPage, PagesSettings settings)
        {
            First = 1;
            Current = currentPage;
            Last = lastPage;
            NextPage = Current.SomeWhen(u => u != lastPage).Map(u => u + 1);

            PreviousPages = 1.RangeTo(Current).SkipLast(1).TakeLast(settings.PreviousPagesLength).ToArray();
            NextPages = lastPage.RangeFrom(Current).Skip(1).Take(settings.NextPagesLength).ToArray();
            NextAuxiliaryPage = NextPages.LastOrNone().Filter(u => u + 1 <= lastPage);
        }

        public Option<int> NextAuxiliaryPage { get; }

        public int[] NextPages { get; }

        public int[] PreviousPages { get; }

        public int Current { get; }

        public int First { get; }

        public int Last { get; }

        public Option<int> NextPage { get; }

        public static Pages Create(int currentPage, int lastPage) => Create(currentPage, lastPage,
            PagesSettings.Default);

        public static Pages Create(int currentPage, int lastPage, PagesSettings settings)
        {
            if (currentPage == 0) throw new ArgumentException($"Invalid value: {currentPage}", nameof(currentPage));

            if (lastPage < currentPage)
            {
                throw new ArgumentsException(
                    $"Invalid last page: {lastPage} It is less than than the current page: {currentPage}",
                    nameof(lastPage), nameof(currentPage));
            }

            return new Pages(currentPage, lastPage, settings);
        }
    }
}