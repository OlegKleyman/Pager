using System.Linq;
using AlphaDev.Paging.Extensions;
using FluentAssertions;
using Xunit;

namespace AlphaDev.Paging.Tests.Unit.Extensions
{
    public class UInt32ExtensionsTests
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 0, 1)]
        public void MaxOfReturnsMaxValueOfUnsignedIntegers(uint val1, uint val2, uint expected)
        {
            val1.MaxOf(val2).Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(0, 1, 0)]
        [InlineData(1, 0, 0)]
        public void MinOfReturnsMinValueOfUnsignedIntegers(uint val1, uint val2, uint expected)
        {
            val1.MinOf(val2).Should().Be(expected);
        }

        [Fact]
        public void RangeToReturnsEnumerableOfUInt32StartingFromTheStartArgumentAndEndingAtTheEndArgument()
        {
            1u.RangeTo(100).Should().BeEquivalentTo(Enumerable.Range(1, 100)).And.BeInAscendingOrder();
        }

        [Fact]
        public void RangeFromReturnsEnumerableOfUInt32StartingFromTheStartArgumentAndEndingAtTheEndArgument()
        {
            100u.RangeFrom(1).Should().BeEquivalentTo(Enumerable.Range(1, 100)).And.BeInAscendingOrder();
        }
    }
}
