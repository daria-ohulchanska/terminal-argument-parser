using FluentAssertions;
using TerminalArgumentParser.Models;
using TerminalArgumentParser.Services.Parsers;

namespace TerminalArgumentParser.Tests.Services
{
    public class FixedArgumentParserTests
    {
        [Fact]
        public void Parse_ShouldParseOptionArgumentsCorrectly()
        {
            string[] args = new[] { "--name", "John", "--repeat", "3" };

            var parser = new FixedArgumentParser<Arguments>();
            var result = parser.Parse(args);

            result.Should().NotBeNull();
            result.Name.Should().Be("John");
            result.Repeat.Should().Be(3);
            result.Verbose.Should().BeFalse();
            result.Capitalize.Should().BeFalse();
        }

        [Fact]
        public void Parse_ShouldParseFlagArgumentsCorrectly()
        {
            string[] args = new[] { "--verbose", "--capitalize" };

            var parser = new FixedArgumentParser<Arguments>();
            var result = parser.Parse(args);

            result.Should().NotBeNull();
            result.Verbose.Should().BeTrue();
            result.Capitalize.Should().BeTrue();
            result.Repeat.Should().Be(1);
            result.Name.Should().BeNull();
        }

        [Fact]
        public void Parse_ShouldReturnDefaultWhenOptionValueIsMissing()
        {
            string[] args = new[] { "--name" };

            var parser = new FixedArgumentParser<Arguments>();
            var result = parser.Parse(args);

            result.Should().BeNull();
        }

        [Fact]
        public void Parse_ShouldHandleUnknownArgumentsGracefully()
        {
            string[] args = new[] { "--unknown" };

            var parser = new FixedArgumentParser<Arguments>();
            var result = parser.Parse(args);

            result.Should().BeNull();
        }

        [Fact]
        public void Parse_ShouldHandleMixOfFlagsAndOptionsCorrectly()
        {
            string[] args = new[] { "--name", "Alice", "--verbose", "--repeat", "5" };

            var parser = new FixedArgumentParser<Arguments>();
            var result = parser.Parse(args);

            result.Should().NotBeNull();
            result.Name.Should().Be("Alice");
            result.Repeat.Should().Be(5);
            result.Verbose.Should().BeTrue();
            result.Capitalize.Should().BeFalse();
        }
    }
}
