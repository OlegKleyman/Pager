﻿using System;
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
            var pager = items.ToPager(5, 10, () => 101);
            pager.Should().BeEquivalentTo(items, options => options.WithStrictOrdering());
        }

        [Fact]
        public void ToPagerReturnsPagerWithCurrentPage()
        {
            var pager = new object[10].ToPager(5, 10, () => 101);
            pager.Pages.Current.Should().Be(5);
        }

        [Fact]
        public void ToPagerReturnsPagerWithLastPageBasedOnTotalItemsAndItemsPerPage()
        {
            var pager = new object[10].ToPager(5, 10, () => 101);
            pager.Pages.Last.Should().Be(11);
        }

        [Fact]
        public async Task ToPagerTaskReturnsPagerItemsInTheSameOrder()
        {
            var items = Enumerable.Range(1, 10).ToArray();
            var pager = await items.ToPager(5, 10, () => Task.FromResult(101));
            pager.Should().BeEquivalentTo(items, options => options.WithStrictOrdering());
        }

        [Fact]
        public async Task ToPagerTaskReturnsPagerWithCurrentPage()
        {
            var pager = await new object[10].ToPager(5, 10, () => Task.FromResult(101));
            pager.Pages.Current.Should().Be(5);
        }

        [Fact]
        public async Task ToPagerTaskReturnsPagerWithLastPageBasedOnTotalItemsAndItemsPerPage()
        {
            var pager = await new object[10].ToPager(5, 10, () => Task.FromResult(101));
            pager.Pages.Last.Should().Be(11);
        }

        [Fact]
        public void ToPagerTaskThrowsArgumentExceptionWhenItemsPerPageIsZero()
        {
            Func<Task> toPager = () => new object[1].ToPager(default, 0, () => Task.FromResult(1));
            toPager.Should().Throw<ArgumentException>().WithMessage("Cannot be '0'. (Parameter 'itemsPerPage')");
        }

        [Fact]
        public void ToPagerTaskThrowsExceptionWhenItemsContainsLessThanItemsPerPageAndItIsNotTheLastPage()
        {
            Func<Task> toPager = () => new object[1].ToPager(1, 10, () => Task.FromResult(100));
            toPager.Should()
                   .Throw<InvalidOperationException>()
                   .WithMessage(
                       "Invalid amount of items. Expected 10, but was 1. Only the last page may contain less than 10 items.");
        }

        [Fact]
        public void ToPagerThrowsArgumentExceptionWhenItemsPerPageIsZero()
        {
            Action toPager = () => new object[1].ToPager(default, 0, () => 1);
            toPager.Should().Throw<ArgumentException>().WithMessage("Cannot be '0'. (Parameter 'itemsPerPage')");
        }

        [Fact]
        public void ToPagerThrowsExceptionWhenItemsContainsLessThanItemsPerPageAndItIsNotTheLastPage()
        {
            Action toPager = () => new object[1].ToPager(1, 10, () => 100);
            toPager.Should()
                   .Throw<InvalidOperationException>()
                   .WithMessage(
                       "Invalid amount of items. Expected 10, but was 1. Only the last page may contain less than 10 items.");
        }
    }
}