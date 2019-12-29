using System;
using FluentAssertions;
using Xunit;

namespace AlphaDev.Paging.Tests.Unit
{
    public class PagesSettingsTests
    {
        [Fact]
        public void DefaultReturnsPagesSettingsWithDefaultSettings()
        {
            PagesSettings.Default.Should()
                         .BeEquivalentTo(new
                         {
                             PreviousPagesLength = ushort.MaxValue,
                             NextPagesLength = ushort.MaxValue,
                             ItemsPerPage = 10
                         });
        }

        [Fact]
        public void ConstructorSetsPreviousPagesLengthAndNextPagesLength()
        {
            new PagesSettings(1,2, 4).Should()
                                  .BeEquivalentTo(new
                                  {
                                      PreviousPagesLength = 1,
                                      NextPagesLength = 2,
                                      ItemsPerPage = 4
                                  });
        }

        [Fact]
        public void ConstructorThrowsArgumentExceptionWhenItemsPerPageIsZero()
        {
            // ReSharper disable once ObjectCreationAsStatement - no need for variable as exception is expected
            Action constructor = () => new PagesSettings(default,default,0);
            constructor.Should().Throw<ArgumentException>().WithMessage("Cannot be '0'. (Parameter 'itemsPerPage')");
        }
    }
}
