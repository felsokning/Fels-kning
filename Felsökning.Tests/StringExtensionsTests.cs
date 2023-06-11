// ----------------------------------------------------------------------
// <copyright file="StringExtensionsTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class StringExtensionsTests
    {
        [DataTestMethod]
        [DataRow(new string[] { })]
        [DataRow(null)]
        public void ToArrayString_ShouldFailForNullOrEmpty(string[] sut)
        {
            var exception = Assert.ThrowsException<StatusException>(() => sut.ToArrayString());

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: The given string was either null, empty, or whitespace");
            exception.InnerException?.Message.Should().Be("Value cannot be null. (Parameter 'value')");
        }

        [TestMethod]
        public void ToArrayString_ShouldSucceed()
        {
            var sut = new string[] { "This", "is", "a", "test." };

            var result = sut.ToArrayString();

            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("This, is, a, test.");
        }

        [TestMethod]
        public void ValidateGoodString()
        {
            string testString = "something";
            testString.Validate();
        }

        [TestMethod]
        public void ValidateBadString()
        {
            string testString = string.Empty;

            var exception = Assert.ThrowsException<StatusException>(() => testString.Validate());

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Contain("Invalid status given in response: The given string was either null, empty, or whitespace");
        }

        [TestMethod]
        public void ValidateGoodLength()
        {
            string testString = "something";
            testString.Validate(9);
        }

        [TestMethod]
        public void ValidateNullLength()
        {
            string testString = string.Empty;

            var exception = Assert.ThrowsException<StatusException>(() => testString.Validate(9));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Contain("Invalid status given in response: The given string was either null, empty, or whitespace");
        }

        [TestMethod]
        public void ValidateBadLength()
        {
            string testString = "something";

            var exception = Assert.ThrowsException<StatusException>(() => testString.Validate(8));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Contain("Invalid status given in response: The given string was not the expected length of 8");
        }
    }
}