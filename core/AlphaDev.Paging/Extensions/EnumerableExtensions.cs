using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaDev.Paging.Extensions
{
    public static class EnumerableExtensions
    {
        public static Task<Pager<T>> ToPager<T>(this IEnumerable<T> items, uint currentPage, uint itemsPerPage,
            Func<Task<int>> totalItems) => ToPager(items, currentPage, itemsPerPage, totalItems,
            PagesSettings.Default);

        public static async Task<Pager<T>> ToPager<T>(this IEnumerable<T> items, uint currentPage, uint itemsPerPage,
            Func<Task<int>> totalItems, PagesSettings settings)
        {
            var pageItems = items.ToArray();

            if (itemsPerPage == 0)
            {
                throw new ArgumentException("Cannot be '0'.", nameof(itemsPerPage));
            }

            var totalPages = (uint)Math.Ceiling((decimal)await totalItems() / itemsPerPage);
            if (totalPages != currentPage && pageItems.Length < itemsPerPage)
            {
                throw new InvalidOperationException(
                    $"Invalid amount of items. Expected {itemsPerPage}, but was {pageItems.Length}. Only the last page may contain less than {itemsPerPage} items.");
            }

            var pages = Pages.Create(currentPage, totalPages, settings);
            return new Pager<T>(pageItems, pages);
        }

        public static Pager<T> ToPager<T>(this IEnumerable<T> items, uint currentPage, uint itemsPerPage,
            Func<int> totalItems)
        {
            return ToPager(items, currentPage, itemsPerPage, () => Task.FromResult(totalItems()))
                   .GetAwaiter()
                   .GetResult();
        }

        public static Pager<T> ToPager<T>(this IEnumerable<T> items, uint currentPage, uint itemsPerPage,
            Func<int> totalItems, PagesSettings settings)
        {
            return ToPager(items, currentPage, itemsPerPage, () => Task.FromResult(totalItems()), settings)
                   .GetAwaiter()
                   .GetResult();
        }
    }
}