using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaDev.Paging.Extensions
{
    public static class EnumerableExtensions
    {
        public static Pager<T> ToPager<T>(this IEnumerable<T> items, in int currentPage, int totalItems) =>
            ToPager(items, currentPage, totalItems, PagesSettings.Default);

        public static Pager<T> ToPager<T>(this IEnumerable<T> items, int currentPage,
            int totalItems, PagesSettings settings)
        {
            var pageItems = items.ToArray();

            var totalPages = (int) Math.Ceiling((decimal) totalItems / settings.ItemsPerPage);
            if (totalPages != currentPage && pageItems.Length < settings.ItemsPerPage)
            {
                throw new InvalidOperationException(
                    $"Invalid amount of items. Expected {settings.ItemsPerPage}, but was {pageItems.Length}. Only the last page may contain less than {settings.ItemsPerPage} items.");
            }

            var pages = Pages.Create(currentPage, totalPages, settings);
            return new Pager<T>(pageItems, pages);
        }
    }
}