using System;
using FluentAssertions.Optional.Extensions;
using Xunit;

namespace FluentAssertions.Optional.Tests.Unit.Extensions
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void AssertEqualityDoesNotThrowExceptionWhenValuesAreEqual()
        {
            Action assertEquality = () => "test".AssertEquality<string, string>("test", options => options);
            assertEquality.Should().NotThrow();
        }

        [Fact]
        public void AssertEqualityThrowsExceptionWhenValuesAreNotEqual()
        {
            Action assertEquality = () => "test".AssertEquality<string, string>("Test", options => options);
            assertEquality.Should()
                          .Throw<Exception>()
                          .WithMessage("Expected string to be \"Test\", " + "but \"test\" differs near \"tes\" " +
                                       "(index 0).\r\n\nWith configuration:\n- Use " +
                                       "declared types and members\r\n- Compare enums " +
                                       "by value\r\n- Include all non-private properties" +
                                       "\r\n- Include all non-private fields\r\n- Match " +
                                       "member by name (or throw)\r\n- Without automatic " +
                                       "conversion.\r\n- Without automatic conversion.\r\n- " +
                                       "Be strict about the order of items in byte arrays\r\n");
        }
    }
}