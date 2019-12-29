using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlphaDev.Paging
{
    public class Pager<T> : IEnumerable<T>
    {
        private readonly ICollection<T> _items;

        public Pager(ICollection<T> items, Pages pages)
        {
            if (!items.Any()) throw new ArgumentException("Value cannot be an empty collection.", nameof(items));
            
            _items = items;

            Pages = pages;
        }

        public Pages Pages { get; }

        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
