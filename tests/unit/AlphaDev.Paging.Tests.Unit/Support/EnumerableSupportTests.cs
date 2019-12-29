using System;
using AlphaDev.Paging.Support;
using FluentAssertions;
using Xunit;

namespace AlphaDev.Paging.Tests.Unit.Support
{
    public class EnumerableSupportTests
    {
        [Fact]
        public void RangeToReturnsEnumerableStartingFromStartArgumentAndEndingAtTheEndArgument()
        {
            EnumerableSupport.RangeTo(7, 18)
                             .Should()
                             .BeEquivalentTo(new[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 })
                             .And.BeInAscendingOrder();
        }


        [Fact]
        public void RangeToThrowsArgumentExceptionWhenStartArgumentIsGreaterThanTheEndArgument()
        {
            Action rangeTo = () => EnumerableSupport.RangeTo(1, 0).GetEnumerator().MoveNext();

            rangeTo.Should()
                   .Throw<ArgumentsException>()
                   .WithMessage("Start is greater than end. (Parameters 'start', 'end')");
        }
    }
}