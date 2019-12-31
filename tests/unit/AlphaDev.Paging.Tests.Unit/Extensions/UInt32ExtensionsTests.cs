using System.Linq;
using AlphaDev.Paging.Extensions;
using FluentAssertions;
using Xunit;

namespace AlphaDev.Paging.Tests.Unit.Extensions
{
    public class Int32ExtensionsTests
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 0, 1)]
        public void MaxOfReturnsMaxValueOfUnsignedIntegers(int val1, int val2, int expected)
        {
            val1.MaxOf(val2).Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(0, 1, 0)]
        [InlineData(1, 0, 0)]
        public void MinOfReturnsMinValueOfUnsignedIntegers(int val1, int val2, int expected)
        {
            val1.MinOf(val2).Should().Be(expected);
        }

        [Fact]
        public void RangeFromReturnsEnumerableOfint32StartingFromTheStartArgumentAndEndingAtTheEndArgument()
        {
            100.RangeFrom(1).Should().BeEquivalentTo(Enumerable.Range(1, 100)).And.BeInAscendingOrder();
        }

        [Fact]
        public void RangeToReturnsEnumerableOfint32StartingFromTheStartArgumentAndEndingAtTheEndArgument()
        {
            1.RangeTo(100).Should().BeEquivalentTo(Enumerable.Range(1, 100)).And.BeInAscendingOrder();
        }
    }
}