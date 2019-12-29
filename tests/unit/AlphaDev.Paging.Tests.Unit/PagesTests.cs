using System;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Optional.Extensions;
using Xunit;

namespace AlphaDev.Paging.Tests.Unit
{
    public class PagesTests
    {
        [Fact]
        public void CreateReturnsNextPageNoneWhenCurrentPageIsLastPage()
        {
            Pages.Create(1, 1).NextPage.Should().BeNone();
        }

        [Fact]
        public void CreateReturnsNextPageWhenCurrentPageIsNotLastPage()
        {
            Pages.Create(1, 2).NextPage.Should().HaveSome().Which.Should().Be(2);
        }

        [Fact]
        public void CreateReturnsPagesWithCurrentPageWhenItIsLessThanOrEqualToLastPage()
        {
            var pages = Pages.Create(2, 10);
            pages.Current.Should().Be(2);
        }

        [Fact]
        public void CreateReturnsPagesWithFirstPageOne()
        {
            var pages = Pages.Create(2, 2);
            pages.First.Should().Be(1);
        }

        [Fact]
        public void CreateReturnsPagesWithLastPageWhenItIsGreaterThanOrEqualToCurrentPage()
        {
            var pages = Pages.Create(2, 10);
            pages.Last.Should().Be(10);
        }

        [Fact]
        public void CreateSetsTheNextPagesThatFollowCurrentPageUpToTheLastPage()
        {
            Pages.Create(17, 200)
                 .NextPages.Should()
                 .BeEquivalentTo(Enumerable.Range(18, 183))
                 .And.BeInAscendingOrder();
        }

        [Fact]
        public void CreateSetsThePreviousPagesBeforeCurrentPage()
        {
            Pages.Create(15, 20)
                 .PreviousPages.Should()
                 .BeEquivalentTo(Enumerable.Range(1, 14))
                 .And.BeInAscendingOrder();
        }

        [Fact]
        public void CreateSetsThePreviousPagesToEmptyArrayWhenCurrentPageIsNone()
        {
            Pages.Create(1, 20).PreviousPages.Should().BeEmpty();
        }

        [Fact]
        public void CreateThrowsArgumentExceptionWhenCurrentPageIsZero()
        {
            Action create = () => Pages.Create(0, 1);
            create.Should()
                  .Throw<ArgumentException>()
                  .WithMessage("Invalid value: 0 (Parameter 'currentPage')");
        }

        [Fact]
        public void CreateThrowsArgumentExceptionWhenLastPageIsLessThanCurrentPage()
        {
            Action create = () => Pages.Create(2, 1);
            create.Should()
                  .Throw<ArgumentsException>()
                  .WithMessage(
                      "Invalid last page: 1 It is less than than the current page: 2 (Parameters 'lastPage', 'currentPage')");
        }
    }
}