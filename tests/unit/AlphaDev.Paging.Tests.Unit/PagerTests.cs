using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace AlphaDev.Paging.Tests.Unit
{
    public class PagerTests
    {
        [Fact]
        public void ConstructorThrowsArgumentExceptionWhenCollectionIsEmpty()
        {
            Action constructor = () => new Pager<string>(Array.Empty<string>(), Pages.Create(1, 1));
            constructor.Should()
                  .Throw<ArgumentException>()
                  .WithMessage("Value cannot be an empty collection. (Parameter 'items')");
        }

        [Fact]
        public void ConstructorInitializesPagerWithPages()
        {
            var pages = Pages.Create(1, 1);
            var pager = new Pager<string>(new[] { string.Empty }, pages);
            pager.Pages.Should().BeEquivalentTo(pages);
        }

        [Fact]
        public void GetEnumeratorOfTGetsEnumeratorOfEnumerable()
        {
            var testValues = Enumerable.Range(0, 10).ToArray();
            var pager = GetPager(testValues, Pages.Create(1, 1));

            using var enumerator = pager.GetEnumerator();
            foreach (var testValue in testValues)
            {
                enumerator.MoveNext().Should().BeTrue();
                enumerator.Current.Should().Be(testValue);
            }

            enumerator.MoveNext().Should().BeFalse();
        }

        [Fact]
        public void GetEnumeratorGetsEnumeratorOfEnumerable()
        {
            var testValues = Enumerable.Range(0, 10).ToArray();
            IEnumerable pager = GetPager(testValues, Pages.Create(1, 1));
            var enumerator = pager.GetEnumerator();

            foreach (var testValue in testValues)
            {
                enumerator.MoveNext().Should().BeTrue();
                enumerator.Current.Should().Be(testValue);
            }

            enumerator.MoveNext().Should().BeFalse();
        }

        [NotNull]
        private static Pager<T> GetPager<T>(ICollection<T> items, Pages pages) => new Pager<T>(items, pages);
    }
}
