using AlphaDev.Paging.Extensions;
using FluentAssertions;
using Xunit;

namespace AlphaDev.Paging.Tests.Unit.Extensions
{
    public class RangeExtensionsTests
    {
        [Fact]
        public void ToEnumerableReturnsEnumerableOfTheRange()
        {
            (1..5).ToEnumerable().Should().BeEquivalentTo(1, 2, 3, 4, 5).And.BeInAscendingOrder();
        }
    }
}
