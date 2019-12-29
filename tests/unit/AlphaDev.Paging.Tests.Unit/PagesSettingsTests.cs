using FluentAssertions;
using Xunit;

namespace AlphaDev.Paging.Tests.Unit
{
    public class PagesSettingsTests
    {
        [Fact]
        public void DefaultReturnsPagesSettingsWithMaxPreviousPagesLengthAndMaxNextPagesLength()
        {
            PagesSettings.Default.Should()
                         .BeEquivalentTo(new
                         {
                             PreviousPagesLength = ushort.MaxValue,
                             NextPagesLength = ushort.MaxValue
                         });
        }

        [Fact]
        public void ConstructorSetsPreviousPagesLengthAndNextPagesLength()
        {
            new PagesSettings(1,2).Should()
                                  .BeEquivalentTo(new
                                  {
                                      PreviousPagesLength = 1,
                                      NextPagesLength = 2
                                  });
        }
    }
}
