using System;
using System.Linq;
using AlphaDev.Paging.Extensions;
using FluentAssertions;
using Xunit;

namespace AlphaDev.Paging.Tests.Unit
{
    public class PagesSettingsTests
    {
        [Fact]
        public void ConstructorSetsPreviousPagesLengthAndNextPagesLength()
        {
            new PagesSettings(1, 2, 4).Should()
                                      .BeEquivalentTo(new
                                      {
                                          PreviousPagesLength = 1,
                                          NextPagesLength = 2,
                                          ItemsPerPage = 4
                                      });
        }

        [Fact]
        public void ConstructorThrowsArgumentExceptionWhenItemsPerPageIsLessThanZero()
        {
            // ReSharper disable once ObjectCreationAsStatement - no need for variable as exception is expected
            Action constructor = () => new PagesSettings(default, default, 0);
            constructor.Should().Throw<ArgumentException>().WithMessage("Cannot be less than '1'. (Parameter 'itemsPerPage')");
        }

        [Fact]
        public void ConstructorThrowsArgumentExceptionWhenPreviousPageLengthIsLessThanZero()
        {
            // ReSharper disable once ObjectCreationAsStatement - no need for variable as exception is expected
            Action constructor = () => new PagesSettings(-1, default, default);
            constructor.Should().Throw<ArgumentException>().WithMessage("Cannot be less than '0'. (Parameter 'previousPagesLength')");
        }

        [Fact]
        public void ConstructorThrowsArgumentExceptionWhenNextPageLengthIsLessThanZero()
        {
            // ReSharper disable once ObjectCreationAsStatement - no need for variable as exception is expected
            Action constructor = () => new PagesSettings(default, -1, default);
            constructor.Should().Throw<ArgumentException>().WithMessage("Cannot be less than '0'. (Parameter 'nextPagesLength')");
        }

        [Fact]
        public void DefaultReturnsPagesSettingsWithDefaultSettings()
        {
            PagesSettings.Default.Should()
                         .BeEquivalentTo(new
                         {
                             PreviousPagesLength = int.MaxValue,
                             NextPagesLength = int.MaxValue,
                             ItemsPerPage = 10
                         });
        }
    }
}