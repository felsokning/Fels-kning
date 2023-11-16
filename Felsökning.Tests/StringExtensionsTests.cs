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
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void GetPostnummerDetails_ShouldFailForNullOrEmpty(string sut)
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => sut.GetPostnummerDetails());

            exception.Should().BeOfType<ArgumentNullException>();
            exception.Message.Should().Be("Value cannot be null. (Parameter 'value')");
        }

        [TestMethod]
        public void GetPostnummerDetails_ShouldFailForTooShort()
        {
            var sut = "1234";

            var exception = Assert.ThrowsException<ArgumentException>(() => sut.GetPostnummerDetails());

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().Be("The parameter supplied was too short to be a valid Swedish postnummer.");
        }

        [TestMethod]
        public void GetPostnummerDetails_ShouldFailForTooLong()
        {
            var sut = "1234567890";

            var exception = Assert.ThrowsException<ArgumentException>(() => sut.GetPostnummerDetails());

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().Be("The parameter supplied was too long to be a valid Swedish postnummer.");
        }

        [TestMethod]
        public void GetPostnummerDetails_ShouldFailForNonNumberCharacters()
        {
            var sut = "1b345";

            var exception = Assert.ThrowsException<ArgumentException>(() => sut.GetPostnummerDetails());

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().Be("The parameter supplied had non-numeric characters, which postnummers do not.");
        }

        [TestMethod]
        public void GetPostnummerDetails_ShouldSucceedForLinköping()
        {
            var sut = "582 22";

            var result = sut.GetPostnummerDetails();

            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Linköping, Brevbäring, Boxpost");
        }

        [TestMethod]
        public void GetPostnummerDetails_ShouldSucceedForCentralStationStockholm()
        {
            var sut = "111 20";

            var result = sut.GetPostnummerDetails();

            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Stockholm, Brevbäring, Svarspost");
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void IsValidPersonNummer_ShouldFailForNullOrEmpty(string sut)
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => sut.IsValidSwedishPersonNummer());

            exception.Should().BeOfType<ArgumentNullException>();
            exception.Message.Should().Be("Value cannot be null. (Parameter 'value')");
        }

        [TestMethod]
        public void IsValidPersonNummer_ShouldFailForIncorrectSize()
        {
            var sut = "This should fail";

            var exception = Assert.ThrowsException<ArgumentException>(() => sut.IsValidSwedishPersonNummer());

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().NotBeNullOrWhiteSpace();
            exception.Message.Should().Be("String is incorrect size: 16");
        }

        [TestMethod]
        public void IsValidPersonNummer_ShouldThrow_ForNoNumbers()
        {
            var sut = "aaaaaaaaaa";

            var exception = Assert.ThrowsException<ArgumentException>(() => sut.IsValidSwedishPersonNummer());

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().NotBeNullOrWhiteSpace();
            exception.Message.Should().Be("Unable to parse 'aaaaaaaaaa' to long.");
        }

        [TestMethod]
        public void IsValidPersonNummer_ShouldFail_ForTenDigits()
        {
            var sut = "867530-9999";

            var result = sut.IsValidSwedishPersonNummer();

            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsValidPersonNummer_ShouldSucceed_ForTenDigits()
        {
            var sut = "811228-9874";

            var result = sut.IsValidSwedishPersonNummer();

            result.Should().BeTrue();
        }

        [TestMethod]
        public void IsValidPersonNummer_ShouldFail_ForTwelveDigits()
        {
            var sut = "19867530-9999";

            var result = sut.IsValidSwedishPersonNummer();

            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsValidPersonNummer_ShouldSucceed_ForTwelveDigits()
        {
            var sut = "19811228-9874";

            var result = sut.IsValidSwedishPersonNummer();

            result.Should().BeTrue();
        }

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