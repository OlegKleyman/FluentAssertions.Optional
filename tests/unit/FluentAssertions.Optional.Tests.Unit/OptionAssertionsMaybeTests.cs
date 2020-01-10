using System;
using System.Drawing;
using JetBrains.Annotations;
using Optional;
using Xunit;

namespace FluentAssertions.Optional.Tests.Unit
{
    public class OptionAssertionsMaybeTests
    {
        [Fact]
        public void BeNoneReturnsAndConstraintWithSelf()
        {
            var assertions = new OptionAssertions<string>(Option.None<string>());
            assertions.BeNone().And.Should().Be(assertions);
        }

        [Fact]
        public void BeNoneThrowsExceptionWhenOptionHasSome()
        {
            var assertions = new OptionAssertions<string?>(default(string).Some());
            Action haveSome = () => assertions.BeNone();
            haveSome.Should().Throw<Exception>().WithMessage("Option has a value.");
        }

        [Fact]
        public void HaveSomeReturnsAndWhichConstraintWithSomeValue()
        {
            var option = "test".Some();
            var assertions = new OptionAssertions<string>(option);
            assertions.HaveSome().Which.Should().Be("test");
        }

        [Fact]
        public void HaveSomeThrowsExceptionWhenOptionHasNone()
        {
            var assertions = new OptionAssertions<string>(Option.None<string>());
            Action haveSome = () => assertions.HaveSome();
            haveSome.Should().Throw<Exception>().WithMessage("Option does not have a value.");
        }

        [Fact]
        public void HasValueEquivalentToThrowsExceptionWhenOptionHasNone()
        {
            var assertions = new OptionAssertions<string>(Option.None<string>());
            Action hasValueEquivalentTo = () => assertions.HasValueEquivalentTo(string.Empty);
            hasValueEquivalentTo.Should().Throw<Exception>().WithMessage("Option does not have a value.");
        }

        [Fact]
        public void HasValueEquivalentToWithOptionsThrowsExceptionWhenOptionHasNone()
        {
            var assertions = new OptionAssertions<string>(Option.None<string>());
            Action hasValueEquivalentTo = () => assertions.HasValueEquivalentTo(string.Empty, options => options);
            hasValueEquivalentTo.Should().Throw<Exception>().WithMessage("Option does not have a value.");
        }

        [Fact]
        public void HasValueEquivalentToThrowsExceptionWhenOptionDoesNotHaveExpectedValue()
        {
            var assertions = new OptionAssertions<string>("test".Some());
            Action hasValueEquivalentTo = () => assertions.HasValueEquivalentTo("Test");
            hasValueEquivalentTo.Should()
                                .Throw<Exception>()
                                .WithMessage("Expected string to be \"Test\", but \"test\" differs near \"tes\" " +
                                             "(index 0).\r\n\nWith configuration:\n- Use declared types and members\r\n- Compare enums " +
                                             "by value\r\n- Include all non-private properties\r\n- Include all non-private fields\r\n- " +
                                             "Match member by name (or throw)\r\n- Without automatic conversion.\r\n- Without automatic conversion.\r\n- Be strict about the order of items in byte arrays\r\n");
        }

        [Fact]
        public void HasValueEquivalentToDoesNotThrowWhenOptionHasExpectedValue()
        {
            var assertions = new OptionAssertions<string>("test".Some());
            Action hasValueEquivalentTo = () => assertions.HasValueEquivalentTo("test");
            hasValueEquivalentTo.Should().NotThrow();
        }

        [Fact]
        public void HasValueEquivalentToWithOptionsDoesNotThrowWhenOptionHasExpectedValue()
        {
            var assertions = CreateOptionAssertions(new { Val1 = "test", Val2 = "tset" });
            Action hasValueEquivalentTo = () => assertions.HasValueEquivalentTo(new { Val1 = "test", Val2 = "tset1" },
                options => options.Excluding(arg => arg.Val2));
            hasValueEquivalentTo.Should().NotThrow();
        }

        [Fact]
        public void HasValueEquivalentToWithOptionsThrowsExceptionWhenOptionDoesNotHaveExpectedValue()
        {
            var assertions = new OptionAssertions<int>(0.Some());
            Action hasValueEquivalentTo = () =>
                assertions.HasValueEquivalentTo(KnownColor.ActiveBorder, options => options.ComparingEnumsByName());
            hasValueEquivalentTo.Should().Throw<Exception>().WithMessage("Expected enum to equal " +
                                                                         "KnownColor.ActiveBorder(1) by name, but found 0.\r\n\nWith " +
                                                                         "configuration:\n- Use declared types and members\r\n- " +
                                                                         "Compare enums by name\r\n- Include all non-private properties" +
                                                                         "\r\n- Include all non-private fields\r\n- Match member by name " +
                                                                         "(or throw)\r\n- Without automatic conversion.\r\n- Without " +
                                                                         "automatic conversion.\r\n- Be strict about the order of items in " +
                                                                         "byte arrays\r\n");
        }

        [NotNull]
        private OptionAssertions<T> CreateOptionAssertions<T>(T value) => new OptionAssertions<T>(value.Some());

        [Fact]
        public void SubjectReturnsOptionUsedInConstructor()
        {
            var option = "test".Some();
            var assertions = new OptionAssertions<string>(option);
            assertions.Subject.Should().Be(option);
        }
    }
}