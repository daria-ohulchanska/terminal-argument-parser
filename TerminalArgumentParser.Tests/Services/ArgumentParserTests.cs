using FluentAssertions;
using TerminalArgumentParser.Services.Parsers;

namespace TerminalArgumentParser.Tests.Services
{
    public class ArgumentParserTests
    {
        [Fact]
        public void Parse_ShouldReturnEmptyDictionary_WhenNoArgumentsProvided()
        {
            string[] args = new string[0];

            var parser = new ArgumentParser();
            var result = parser.Parse(args);

            result.Should().BeEmpty();
        }

        [Fact]
        public void Parse_ShouldParseSingleOptionWithValue()
        {
            string[] args = new[] { "--name", "Bella" };

            var parser = new ArgumentParser();
            var result = parser.Parse(args);

            result.Should().ContainKey("name").WhoseValue.Should().Be("Bella");
        }

        [Fact]
        public void Parse_ShouldHandleMultipleOptionsWithValues()
        {
            string[] args = new[] { "--name", "Bella", "--age", "30" };

            var parser = new ArgumentParser();
            var result = parser.Parse(args);

            result.Should().HaveCount(2)
                .And.Contain(new KeyValuePair<string, string>("name", "Bella"))
                .And.Contain(new KeyValuePair<string, string>("age", "30"));
        }

        [Fact]
        public void Parse_ShouldHandleOptionWithoutValue()
        {
            string[] args = new[] { "--verbose" };

            var parser = new ArgumentParser();
            var result = parser.Parse(args);

            result.Should().ContainKey("verbose").WhoseValue.Should().BeEmpty();
        }

        [Fact]
        public void Parse_ShouldHandleMixedOptionsWithAndWithoutValues()
        {
            string[] args = new[] { "--name", "Bella", "--verbose", "--age", "30" };

            var parser = new ArgumentParser();
            var result = parser.Parse(args);

            result.Should().HaveCount(3)
                .And.Contain(new KeyValuePair<string, string>("name", "Bella"))
                .And.Contain(new KeyValuePair<string, string>("verbose", string.Empty))
                .And.Contain(new KeyValuePair<string, string>("age", "30"));
        }
    }
}
