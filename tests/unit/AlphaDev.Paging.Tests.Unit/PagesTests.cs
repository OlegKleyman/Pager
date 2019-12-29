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
        public void CreateSetsTheNextAuxiliaryPageToNone()
        {
            Pages.Create(20, 20)
                 .NextAuxiliaryPage.Should()
                 .BeNone();
        }

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
        public void CreateSetsTheNextPagesToEmptyArrayWhenCurrentPageIsLastPage()
        {
            Pages.Create(200, 200)
                 .NextPages.Should()
                 .BeEmpty();
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

        [Fact]
        public void CreateWithSettingsReturnsNextPageNoneWhenCurrentPageIsLastPage()
        {
            Pages.Create(1, 1, PagesSettings.Default).NextPage.Should().BeNone();
        }

        [Fact]
        public void CreateWithSettingsReturnsNextPageWhenCurrentPageIsNotLastPage()
        {
            Pages.Create(1, 2, PagesSettings.Default).NextPage.Should().HaveSome().Which.Should().Be(2);
        }

        [Fact]
        public void CreateWithSettingsReturnsPagesWithCurrentPageWhenItIsLessThanOrEqualToLastPage()
        {
            var pages = Pages.Create(2, 10, PagesSettings.Default);
            pages.Current.Should().Be(2);
        }

        [Fact]
        public void CreateWithSettingsReturnsPagesWithFirstPageOne()
        {
            var pages = Pages.Create(2, 2, PagesSettings.Default);
            pages.First.Should().Be(1);
        }

        [Fact]
        public void CreateWithSettingsReturnsPagesWithLastPageWhenItIsGreaterThanOrEqualToCurrentPage()
        {
            var pages = Pages.Create(2, 10, PagesSettings.Default);
            pages.Last.Should().Be(10);
        }

        [Fact]
        public void CreateWithSettingsSetsTheNextPagesThatFollowCurrentPageUpToTheNextPageLength()
        {
            Pages.Create(17, 200, new PagesSettings(ushort.MaxValue, 4, 1))
                 .NextPages.Should()
                 .BeEquivalentTo(Enumerable.Range(18, 4))
                 .And.BeInAscendingOrder();
        }

        [Fact]
        public void CreateWithSettingsSetsTheNextPagesToEmptyArrayWhenCurrentPageIsLastPage()
        {
            Pages.Create(200, 200, new PagesSettings(ushort.MaxValue, ushort.MaxValue, 1))
                 .NextPages.Should()
                 .BeEmpty();
        }

        [Fact]
        public void CreateWithSettingsSetsThePreviousPagesBeforeCurrentPageUpToThePreviousPageLength()
        {
            Pages.Create(15, 20, new PagesSettings(4, ushort.MaxValue, 1))
                 .PreviousPages.Should()
                 .BeEquivalentTo(Enumerable.Range(11, 4))
                 .And.BeInAscendingOrder();
        }

        [Fact]
        public void CreateWithSettingsSetsThePreviousPagesToEmptyArrayWhenCurrentPageIsNone()
        {
            Pages.Create(1, 20, new PagesSettings(ushort.MaxValue, ushort.MaxValue,1)).PreviousPages.Should().BeEmpty();
        }

        [Fact]
        public void CreateWithSettingsSetsTheNextAuxiliaryPageToTheFirstNoneVisiblePageWhenItIsNotPassedTheLastPage()
        {
            Pages.Create(1, 20, new PagesSettings(ushort.MaxValue, 7, 1))
                 .NextAuxiliaryPage.Should()
                 .HaveSome()
                 .Which.Should()
                 .Be(8);
        }

        [Fact]
        public void CreateWithSettingsSetsTheNextAuxiliaryPageToNoneWhenItIsPassedTheLastNextVisiblePage()
        {
            Pages.Create(1, 20, new PagesSettings(ushort.MaxValue, 19,1))
                 .NextAuxiliaryPage.Should()
                 .BeNone();
        }

        [Fact]
        public void CreateWithSettingsThrowsArgumentExceptionWhenCurrentPageIsZero()
        {
            Action create = () => Pages.Create(0, 1, PagesSettings.Default);
            create.Should()
                  .Throw<ArgumentException>()
                  .WithMessage("Invalid value: 0 (Parameter 'currentPage')");
        }

        [Fact]
        public void CreateWithSettingsThrowsArgumentExceptionWhenLastPageIsLessThanCurrentPage()
        {
            Action create = () => Pages.Create(2, 1, PagesSettings.Default);
            create.Should()
                  .Throw<ArgumentsException>()
                  .WithMessage(
                      "Invalid last page: 1 It is less than than the current page: 2 (Parameters 'lastPage', 'currentPage')");
        }
    }
}