using System;
using System.Drawing;
using JetBrains.Annotations;
using Optional;
using Xunit;

namespace FluentAssertions.Optional.Tests.Unit
{
    public class OptionAssertionsEitherTests
    {
        [NotNull]
        private OptionAssertions<T, object> CreateOptionAssertionsWithSome<T>(T value) =>
            new OptionAssertions<T, object>(Option.Some<T, object>(value));

        [NotNull]
        private OptionAssertions<object, T> CreateOptionAssertionsWithNone<T>(T value) =>
            new OptionAssertions<object, T>(Option.None<object, T>(value));

        [Fact]
        public void BeNoneReturnsAndWhichConstraintWithExceptionValueWhenOptionIsNone()
        {
            var option = Option.None<string, string>("test");
            var assertions = new OptionAssertions<string, string>(option);
            assertions.BeNone().Which.Should().Be("test");
        }

        [Fact]
        public void BeNoneThrowsExceptionWhenOptionHasSome()
        {
            var assertions = new OptionAssertions<string?, string?>(Option.Some<string?, string?>(default));
            Action haveSome = () => assertions.BeNone();
            haveSome.Should().Throw<Exception>().WithMessage("Option has a value.");
        }

        [Fact]
        public void HasExceptionEquivalentToDoesNotThrowWhenOptionHasExpectedException()
        {
            var assertions = new OptionAssertions<string, string>(Option.None<string, string>("test"));
            Action hasExceptionEquivalentTo = () => assertions.HasExceptionEquivalentTo("test");
            hasExceptionEquivalentTo.Should().NotThrow();
        }

        [Fact]
        public void HasExceptionEquivalentToThrowsExceptionWhenOptionDoesNotHaveExpectedValue()
        {
            var assertions = new OptionAssertions<string, string>(Option.None<string, string>("test"));
            Action hasExceptionEquivalentTo = () => assertions.HasExceptionEquivalentTo("Test");
            hasExceptionEquivalentTo.Should()
                                    .Throw<Exception>()
                                    .WithMessage("Expected string to be \"Test\", but \"test\" differs near \"tes\" " +
                                                 "(index 0).\r\n\nWith configuration:\n- Use declared types and members\r\n- Compare enums " +
                                                 "by value\r\n- Include all non-private properties\r\n- Include all non-private fields\r\n- " +
                                                 "Match member by name (or throw)\r\n- Without automatic conversion.\r\n- Without automatic conversion.\r\n- Be strict about the order of items in byte arrays\r\n");
        }

        [Fact]
        public void HasExceptionEquivalentToThrowsExceptionWhenOptionHasSome()
        {
            var assertions = new OptionAssertions<string?, string>(Option.Some<string?, string>(default));
            Action hasExceptionEquivalentTo = () => assertions.HasExceptionEquivalentTo(string.Empty);
            hasExceptionEquivalentTo.Should().Throw<Exception>().WithMessage("Option has a value.");
        }

        [Fact]
        public void HasExceptionEquivalentToWithOptionsDoesNotThrowWhenOptionHasExpectedValue()
        {
            var assertions = CreateOptionAssertionsWithNone(new { Val1 = "test", Val2 = "tset" });
            Action hasExceptionEquivalentTo = () => assertions.HasExceptionEquivalentTo(
                new { Val1 = "test", Val2 = "tset1" },
                options => options.Excluding(arg => arg.Val2));
            hasExceptionEquivalentTo.Should().NotThrow();
        }

        [Fact]
        public void HasExceptionEquivalentToWithOptionsThrowsExceptionWhenOptionDoesNotHaveExpectedValue()
        {
            var assertions = new OptionAssertions<object, int>(Option.None<object, int>(0));
            Action hasExceptionEquivalentTo = () =>
                assertions.HasExceptionEquivalentTo(KnownColor.ActiveBorder, options => options.ComparingEnumsByName());
            hasExceptionEquivalentTo.Should()
                                    .Throw<Exception>()
                                    .WithMessage("Expected enum to equal " +
                                                 "KnownColor.ActiveBorder(1) by name, but found 0.\r\n\nWith " +
                                                 "configuration:\n- Use declared types and members\r\n- " +
                                                 "Compare enums by name\r\n- Include all non-private properties" +
                                                 "\r\n- Include all non-private fields\r\n- Match member by name " +
                                                 "(or throw)\r\n- Without automatic conversion.\r\n- Without " +
                                                 "automatic conversion.\r\n- Be strict about the order of items in " +
                                                 "byte arrays\r\n");
        }

        [Fact]
        public void HasExceptionEquivalentToWithOptionsThrowsExceptionWhenOptionHasSome()
        {
            var assertions = new OptionAssertions<string?, string>(Option.Some<string?, string>(default));
            Action hasExceptionEquivalentTo =
                () => assertions.HasExceptionEquivalentTo(string.Empty, options => options);
            hasExceptionEquivalentTo.Should().Throw<Exception>().WithMessage("Option has a value.");
        }

        [Fact]
        public void HasValueEquivalentToDoesNotThrowWhenOptionHasExpectedValue()
        {
            var assertions = new OptionAssertions<string, string>(Option.Some<string, string>("test"));
            Action hasValueEquivalentTo = () => assertions.HasValueEquivalentTo("test");
            hasValueEquivalentTo.Should().NotThrow();
        }

        [Fact]
        public void HasValueEquivalentToEquivalentToWithOptionsThrowsExceptionWhenOptionDoesNotHaveExpectedValue()
        {
            var assertions = new OptionAssertions<int, object>(Option.Some<int, object>(0));
            Action hasValueEquivalentTo = () =>
                assertions.HasValueEquivalentTo(KnownColor.ActiveBorder, options => options.ComparingEnumsByName());
            hasValueEquivalentTo.Should()
                                .Throw<Exception>()
                                .WithMessage("Expected enum to equal " +
                                             "KnownColor.ActiveBorder(1) by name, but found 0.\r\n\nWith " +
                                             "configuration:\n- Use declared types and members\r\n- " +
                                             "Compare enums by name\r\n- Include all non-private properties" +
                                             "\r\n- Include all non-private fields\r\n- Match member by name " +
                                             "(or throw)\r\n- Without automatic conversion.\r\n- Without " +
                                             "automatic conversion.\r\n- Be strict about the order of items in " +
                                             "byte arrays\r\n");
        }

        [Fact]
        public void HasValueEquivalentToThrowsExceptionWhenOptionDoesNotHaveExpectedValue()
        {
            var assertions = new OptionAssertions<string, string>(Option.Some<string, string>("test"));
            Action hasValueEquivalentTo = () => assertions.HasValueEquivalentTo("Test");
            hasValueEquivalentTo.Should()
                                .Throw<Exception>()
                                .WithMessage("Expected string to be \"Test\", but \"test\" differs near \"tes\" " +
                                             "(index 0).\r\n\nWith configuration:\n- Use declared types and members\r\n- Compare enums " +
                                             "by value\r\n- Include all non-private properties\r\n- Include all non-private fields\r\n- " +
                                             "Match member by name (or throw)\r\n- Without automatic conversion.\r\n- Without automatic conversion.\r\n- Be strict about the order of items in byte arrays\r\n");
        }

        [Fact]
        public void HasValueEquivalentToThrowsExceptionWhenOptionHasNone()
        {
            var assertions = new OptionAssertions<string, string?>(Option.None<string, string?>(default));
            Action hasValueEquivalentTo = () => assertions.HasValueEquivalentTo(string.Empty);
            hasValueEquivalentTo.Should().Throw<Exception>().WithMessage("Option does not have a value.");
        }

        [Fact]
        public void HasValueEquivalentToWithOptionsDoesNotThrowWhenOptionHasExpectedValue()
        {
            var assertions = CreateOptionAssertionsWithSome(new { Val1 = "test", Val2 = "tset" });
            Action hasValueEquivalentTo = () => assertions.HasValueEquivalentTo(new { Val1 = "test", Val2 = "tset1" },
                options => options.Excluding(arg => arg.Val2));
            hasValueEquivalentTo.Should().NotThrow();
        }

        [Fact]
        public void HasValueEquivalentToWithOptionsThrowsExceptionWhenOptionHasNone()
        {
            var assertions = new OptionAssertions<string, string?>(Option.None<string, string?>(default));
            Action hasValueEquivalentTo = () => assertions.HasValueEquivalentTo(string.Empty, options => options);
            hasValueEquivalentTo.Should().Throw<Exception>().WithMessage("Option does not have a value.");
        }

        [Fact]
        public void HaveSomeReturnsAndWhichConstraintWithSomeValue()
        {
            var option = Option.Some<string, string>("test");
            var assertions = new OptionAssertions<string, string>(option);
            assertions.HaveSome().Which.Should().Be("test");
        }

        [Fact]
        public void HaveSomeThrowsExceptionWhenOptionHasNone()
        {
            var assertions = new OptionAssertions<string, string?>(Option.None<string, string?>(default));
            Action haveSome = () => assertions.HaveSome();
            haveSome.Should().Throw<Exception>().WithMessage("Option does not have a value.");
        }

        [Fact]
        public void SubjectReturnsOptionUsedInConstructor()
        {
            var option = Option.Some<string, string>(string.Empty);
            var assertions = new OptionAssertions<string, string>(option);
            assertions.Subject.Should().Be(option);
        }
    }
}