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
        public void IsLower_ShouldFailForNullEmptyOrWhitespace(string sut)
        {
            var exception = Assert.ThrowsExactly<ArgumentNullException>(() => sut.IsLower());

            exception.Should().BeOfType<ArgumentNullException>();
            exception.Message.Should().Be("Value cannot be null. (Parameter 'value')");
        }

        [TestMethod]
        public void IsLower_ShouldSucceed()
        {
            var firstSut = "tEsTiNg";

            var firstResult = firstSut.IsLower();
            firstResult.Should().BeFalse();

            var secondSut = "testing";

            var secondResult = secondSut.IsLower();
            secondResult.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void IsUpper_ShouldFailForNullEmptyOrWhitespace(string sut)
        {
            var exception = Assert.ThrowsExactly<ArgumentNullException>(() => sut.IsUpper());

            exception.Should().BeOfType<ArgumentNullException>();
            exception.Message.Should().Be("Value cannot be null. (Parameter 'value')");
        }

        [TestMethod]
        public void IsUpper_ShouldSucceed()
        {
            var firstSut = "TESTING";

            var firstResult = firstSut.IsUpper();
            firstResult.Should().BeTrue();

            var secondSut = "testing";

            var secondResult = secondSut.IsUpper();
            secondResult.Should().BeFalse();

            var thirdSut = "teSTing";

            var thirdResult = thirdSut.IsUpper();
            thirdResult.Should().BeFalse();

            var fourthSut = "TEsting";

            var fourthResult = fourthSut.IsUpper();
            fourthResult.Should().BeFalse();
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void GetPostnummerDetails_ShouldFailForNullOrEmpty(string sut)
        {
            var exception = Assert.ThrowsExactly<ArgumentNullException>(() => sut.GetPostnummerDetails());

            exception.Should().BeOfType<ArgumentNullException>();
            exception.Message.Should().Be("Value cannot be null. (Parameter 'value')");
        }

        [TestMethod]
        public void GetPostnummerDetails_ShouldFailForTooShort()
        {
            var sut = "1234";

            var exception = Assert.ThrowsExactly<ArgumentException>(() => sut.GetPostnummerDetails());

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().Be("The parameter supplied was too short to be a valid Swedish postnummer.");
        }

        [TestMethod]
        public void GetPostnummerDetails_ShouldFailForTooLong()
        {
            var sut = "1234567890";

            var exception = Assert.ThrowsExactly<ArgumentException>(() => sut.GetPostnummerDetails());

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().Be("The parameter supplied was too long to be a valid Swedish postnummer.");
        }

        [TestMethod]
        public void GetPostnummerDetails_ShouldFailForNonNumberCharacters()
        {
            var sut = "1b345";

            var exception = Assert.ThrowsExactly<ArgumentException>(() => sut.GetPostnummerDetails());

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
            var exception = Assert.ThrowsExactly<ArgumentNullException>(() => sut.IsValidSwedishPersonNummer());

            exception.Should().BeOfType<ArgumentNullException>();
            exception.Message.Should().Be("Value cannot be null. (Parameter 'value')");
        }

        [TestMethod]
        public void IsValidPersonNummer_ShouldFailForIncorrectSize()
        {
            var sut = "This should fail";

            var result = sut.IsValidSwedishPersonNummer();

            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsValidPersonNummer_ShouldThrow_ForNoNumbers()
        {
            var sut = "aaaaaaaaaa";

            var result = sut.IsValidSwedishPersonNummer();

            result.Should().BeFalse();
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

        [DataTestMethod]
        [DataRow("189001019802")]
        [DataRow("189001029819")]
        [DataRow("189001039800")]
        [DataRow("189001049817")]
        [DataRow("189001059808")]
        [DataRow("189001069815")]
        [DataRow("189001079806")]
        [DataRow("189001089813")]
        [DataRow("189001099804")]
        [DataRow("189001109819")]
        [DataRow("189001119800")]
        [DataRow("189001129817")]
        [DataRow("189001139808")]
        [DataRow("189001149815")]
        [DataRow("189001159806")]
        [DataRow("189001169813")]
        [DataRow("189001179804")]
        [DataRow("189001189811")]
        [DataRow("189001199802")]
        [DataRow("189001209817")]
        [DataRow("189001219808")]
        [DataRow("189001229815")]
        [DataRow("189001239806")]
        [DataRow("189001249813")]
        [DataRow("189001259804")]
        [DataRow("189001269811")]
        [DataRow("189001279802")]
        [DataRow("189001289819")]
        [DataRow("189001299800")]
        [DataRow("189001309815")]
        [DataRow("189001319806")]
        [DataRow("189002019819")]
        [DataRow("189002029800")]
        [DataRow("189002039817")]
        [DataRow("189002049808")]
        [DataRow("189002059815")]
        [DataRow("189002069806")]
        [DataRow("189002079813")]
        [DataRow("189002089804")]
        [DataRow("189002099811")]
        [DataRow("189002109800")]
        [DataRow("189002119817")]
        [DataRow("189002129808")]
        [DataRow("189002139815")]
        [DataRow("189002149806")]
        [DataRow("189002159813")]
        [DataRow("189002169804")]
        [DataRow("189002179811")]
        [DataRow("189002189802")]
        [DataRow("189002199819")]
        [DataRow("189002209808")]
        [DataRow("189002219815")]
        [DataRow("189002229806")]
        [DataRow("189002239813")]
        [DataRow("189002249804")]
        [DataRow("189002259811")]
        [DataRow("189002269802")]
        [DataRow("189002279819")]
        [DataRow("189002289800")]
        [DataRow("189003019818")]
        [DataRow("189003029809")]
        [DataRow("189003039816")]
        [DataRow("189003049807")]
        [DataRow("189003059814")]
        [DataRow("189003069805")]
        [DataRow("189003079812")]
        [DataRow("189003089803")]
        [DataRow("189003099810")]
        [DataRow("189003109809")]
        [DataRow("189003119816")]
        [DataRow("189003129807")]
        [DataRow("189003139814")]
        [DataRow("189003149805")]
        [DataRow("189003159812")]
        [DataRow("189003169803")]
        [DataRow("189003179810")]
        [DataRow("189003189801")]
        [DataRow("189003199818")]
        [DataRow("189003209807")]
        [DataRow("189003219814")]
        [DataRow("189003229805")]
        [DataRow("189003239812")]
        [DataRow("189003249803")]
        [DataRow("189003259810")]
        [DataRow("189003269801")]
        [DataRow("189003279818")]
        [DataRow("189003289809")]
        [DataRow("189003299816")]
        [DataRow("189003309805")]
        [DataRow("189003319812")]
        [DataRow("189004019809")]
        [DataRow("189004029816")]
        [DataRow("189004039807")]
        [DataRow("189004049814")]
        [DataRow("189004059805")]
        [DataRow("189004069812")]
        [DataRow("189004079803")]
        [DataRow("189004089810")]
        [DataRow("189004099801")]
        [DataRow("189004109816")]
        [DataRow("201311122392")]
        [DataRow("201311122384")]
        [DataRow("201311132383")]
        [DataRow("201311132391")]
        [DataRow("201311142390")]
        [DataRow("201311142382")]
        [DataRow("201311152399")]
        [DataRow("201311152381")]
        [DataRow("201311162380")]
        [DataRow("201311162398")]
        [DataRow("201311172389")]
        [DataRow("201311172397")]
        [DataRow("201311182396")]
        [DataRow("201311182388")]
        [DataRow("201311192387")]
        [DataRow("201311192395")]
        [DataRow("201311202392")]
        [DataRow("201311202384")]
        [DataRow("201311212383")]
        [DataRow("201311212391")]
        [DataRow("201311222390")]
        [DataRow("201311222382")]
        [DataRow("201311232399")]
        [DataRow("201311232381")]
        [DataRow("201311242398")]
        [DataRow("201311242380")]
        [DataRow("201311252397")]
        [DataRow("201311252389")]
        [DataRow("201311262388")]
        [DataRow("201311262396")]
        [DataRow("201311272395")]
        [DataRow("201311272387")]
        [DataRow("201311282394")]
        [DataRow("201311282386")]
        [DataRow("201311292385")]
        [DataRow("201311292393")]
        [DataRow("201311302390")]
        [DataRow("201311302382")]
        [DataRow("201312012386")]
        [DataRow("201312012394")]
        [DataRow("201312022393")]
        [DataRow("201312022385")]
        [DataRow("201312032392")]
        [DataRow("201312032384")]
        [DataRow("201312042391")]
        [DataRow("201312042383")]
        [DataRow("201312052390")]
        [DataRow("201312052382")]
        [DataRow("201312062399")]
        [DataRow("201312062381")]
        [DataRow("201312072380")]
        [DataRow("201312072398")]
        [DataRow("201312082389")]
        [DataRow("201312082397")]
        [DataRow("201312092388")]
        [DataRow("201312092396")]
        [DataRow("201312102393")]
        [DataRow("201312102385")]
        [DataRow("201312112384")]
        [DataRow("201312112392")]
        [DataRow("201312122383")]
        [DataRow("201312122391")]
        [DataRow("201312132390")]
        [DataRow("201312132382")]
        [DataRow("201312142399")]
        [DataRow("201312142381")]
        [DataRow("201312152380")]
        [DataRow("201312152398")]
        [DataRow("201312162389")]
        [DataRow("201312162397")]
        [DataRow("201312172388")]
        [DataRow("201312172396")]
        [DataRow("201312182395")]
        [DataRow("201312182387")]
        [DataRow("201312192386")]
        [DataRow("201312192394")]
        [DataRow("201312202383")]
        [DataRow("201312202391")]
        [DataRow("201312212390")]
        [DataRow("201312212382")]
        [DataRow("201312222381")]
        [DataRow("201312222399")]
        [DataRow("201312232380")]
        [DataRow("201312232398")]
        [DataRow("201312242389")]
        [DataRow("201312242397")]
        [DataRow("201312252396")]
        [DataRow("201312252388")]
        [DataRow("201312262395")]
        [DataRow("201312262387")]
        [DataRow("201312272394")]
        [DataRow("201312272386")]
        [DataRow("201312282393")]
        [DataRow("201312282385")]
        [DataRow("201312292392")]
        [DataRow("201312292384")]
        [DataRow("201312302399")]
        [DataRow("201312302381")]
        [DataRow("201312312380")]
        [DataRow("201312312398")]
        public void IsValidPersonNummer_ShouldSucceed_ForNonSeparatedDigits(string sut)
        {
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

        [TestMethod]
        public void IsValidPersnonNummer_ShouldSucceed_ForTwelveDigits_Over100()
        {
            var sut = "231202+9289";

            var result = sut.IsValidSwedishPersonNummer();

            result.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow(new string[] { })]
        [DataRow(null)]
        public void ToArrayString_ShouldFailForNullOrEmpty(string[] sut)
        {
            var exception = Assert.ThrowsExactly<StatusException>(() => sut.ToArrayString());

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

            var exception = Assert.ThrowsExactly<StatusException>(() => testString.Validate());

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

            var exception = Assert.ThrowsExactly<StatusException>(() => testString.Validate(9));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Contain("Invalid status given in response: The given string was either null, empty, or whitespace");
        }

        [TestMethod]
        public void ValidateBadLength()
        {
            string testString = "something";

            var exception = Assert.ThrowsExactly<StatusException>(() => testString.Validate(8));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Contain("Invalid status given in response: The given string was not the expected length of 8");
        }
    }
}