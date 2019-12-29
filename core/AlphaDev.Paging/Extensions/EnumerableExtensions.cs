using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaDev.Paging.Extensions
{
    public static class EnumerableExtensions
    {
        public static Task<Pager<T>> ToPager<T>(this IEnumerable<T> items, uint currentPage,
            Func<Task<int>> totalItems) => ToPager(items, currentPage, totalItems,
            PagesSettings.Default);

        public static async Task<Pager<T>> ToPager<T>(this IEnumerable<T> items, uint currentPage,
            Func<Task<int>> totalItems, PagesSettings settings)
        {
            var pageItems = items.ToArray();

            var totalPages = (uint) Math.Ceiling((decimal) await totalItems() / settings.ItemsPerPage);
            if (totalPages != currentPage && pageItems.LongLength < settings.ItemsPerPage)
            {
                throw new InvalidOperationException(
                    $"Invalid amount of items. Expected {settings.ItemsPerPage}, but was {pageItems.Length}. Only the last page may contain less than {settings.ItemsPerPage} items.");
            }

            var pages = Pages.Create(currentPage, totalPages, settings);
            return new Pager<T>(pageItems, pages);
        }

        public static Pager<T> ToPager<T>(this IEnumerable<T> items, uint currentPage,
            Func<int> totalItems)
        {
            return ToPager(items, currentPage, () => Task.FromResult(totalItems()))
                   .GetAwaiter()
                   .GetResult();
        }

        public static Pager<T> ToPager<T>(this IEnumerable<T> items, uint currentPage,
            Func<int> totalItems, PagesSettings settings)
        {
            return ToPager(items, currentPage, () => Task.FromResult(totalItems()), settings)
                   .GetAwaiter()
                   .GetResult();
        }
    }
}