using System;
using System.Linq;
using System.Threading.Tasks;
using AlphaDev.Paging.Extensions;
using FluentAssertions;
using Xunit;

namespace AlphaDev.Paging.Tests.Unit.Extensions
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void ToPagerReturnsPagerItemsInTheSameOrder()
        {
            var items = Enumerable.Range(1, 10).ToArray();
            var pager = items.ToPager(5, 101);
            pager.Should().BeEquivalentTo(items, options => options.WithStrictOrdering());
        }

        [Fact]
        public void ToPagerReturnsPagerWithCurrentPage()
        {
            var pager = new object[10].ToPager(5, 101);
            pager.Pages.Current.Should().Be(5);
        }

        [Fact]
        public void ToPagerReturnsPagerWithLastPageBasedOnTotalItemsAndItemsPerPage()
        {
            var pager = new object[10].ToPager(5, 101);
            pager.Pages.Last.Should().Be(11);
        }

        [Fact]
        public void ToPagerThrowsExceptionWhenItemsContainsLessThanItemsPerPageAndItIsNotTheLastPage()
        {
            Action toPager = () => new object[1].ToPager(1, 100);
            toPager.Should()
                   .Throw<InvalidOperationException>()
                   .WithMessage(
                       "Invalid amount of items. Expected 10, but was 1. Only the last page may contain less than 10 items.");
        }

        [Fact]
        public void ToPagerWithSettingsReturnsPagerBasedOnSettings()
        {
            var items = Enumerable.Range(1, 10).ToArray();
            var pager = items.ToPager(5, 101, new PagesSettings(2, 4, 10));
            pager.Pages.PreviousPages.Should().BeEquivalentTo(new[] { 4, 3 });
            pager.Pages.NextPages.Should().BeEquivalentTo(new[] { 6, 7, 8, 9 });
        }

        [Fact]
        public void ToPagerWithSettingsReturnsPagerItemsInTheSameOrder()
        {
            var items = Enumerable.Range(1, 10).ToArray();
            var pager = items.ToPager(5, 101, PagesSettings.Default);
            pager.Should().BeEquivalentTo(items, options => options.WithStrictOrdering());
        }

        [Fact]
        public void ToPagerWithSettingsReturnsPagerWithCurrentPage()
        {
            var pager = new object[10].ToPager(5, 101, PagesSettings.Default);
            pager.Pages.Current.Should().Be(5);
        }

        [Fact]
        public void ToPagerWithSettingsReturnsPagerWithLastPageBasedOnTotalItemsAndItemsPerPage()
        {
            var pager = new object[10].ToPager(5, 101, PagesSettings.Default);
            pager.Pages.Last.Should().Be(11);
        }

        [Fact]
        public void ToPagerWithSettingsThrowsExceptionWhenItemsContainsLessThanItemsPerPageAndItIsNotTheLastPage()
        {
            Action toPager = () =>
                new object[1].ToPager(1, 100, PagesSettings.Default);
            toPager.Should()
                   .Throw<InvalidOperationException>()
                   .WithMessage(
                       "Invalid amount of items. Expected 10, but was 1. Only the last page may contain less than 10 items.");
        }
    }
}