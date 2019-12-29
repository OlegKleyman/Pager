using FluentAssertions;
using Xunit;

namespace AlphaDev.Paging.Tests.Unit
{
    public class ArgumentsExceptionTests
    {
        [Fact]
        public void ConstructorInitializesMessageWithWithMessageAndDelimitedListOfParameterNames()
        {
            var exception = new ArgumentsException("test", "param1", "param2", "param3");
            exception.Message.Should().BeEquivalentTo("test (Parameters 'param1', 'param2', 'param3')");
        }

        [Fact]
        public void ConstructorInitializesParamNamesWithParameterNamesArgument()
        {
            var exception = new ArgumentsException(string.Empty, "param1", "param2", "param3");
            exception.ParamNames.Should().BeEquivalentTo("param1", "param2", "param3");
        }

        [Fact]
        public void ConstructorInitializesParamNameWithDelimitedListOfParameterNames()
        {
            var exception = new ArgumentsException(string.Empty, "param1", "param2", "param3");
            exception.ParamName.Should().BeEquivalentTo("param1, param2, param3");
        }
    }
}