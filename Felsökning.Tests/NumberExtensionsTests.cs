// ----------------------------------------------------------------------
// <copyright file="NumberExtensionsTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class NumberExtensionsTests
    {
        [TestMethod]
        public void IsValidPersonNummer_Long_Valid()
        {
            long validPersonNummer = 198112289874; // Example of a valid Swedish personnummer
            bool result = validPersonNummer.IsValidPersonNummer();
            result.Should().BeTrue();
        }

        [TestMethod]
        public void IsValidPersonNummer_Long_Invalid()
        {
            long invalidPersonNummer = 198501011235; // Example of an invalid Swedish personnummer
            bool result = invalidPersonNummer.IsValidPersonNummer();
            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsValidPersonNummer_Int_Valid()
        {
            unchecked
            {
                int validPersonNummer = (int)8112289874; // Example of a valid Swedish personnummer
                bool result = validPersonNummer.IsValidPersonNummer();
                result.Should().BeFalse();
            }
        }

        [TestMethod]
        public void IsValidPersonNummer_Int_Invalid()
        {
            unchecked
            {
                int invalidPersonNummer = (int)8501011235; // Example of an invalid Swedish personnummer
                bool result = invalidPersonNummer.IsValidPersonNummer();
                result.Should().BeFalse();
            }
        }

        [TestMethod]
        public void IsValidPersonNummer_Short_Valid()
        {
            short validPersonNummer = 1234; // Example of a valid Swedish personnummer
            bool result = validPersonNummer.IsValidPersonNummer();
            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsValidPersonNummer_Short_Invalid()
        {
            short invalidPersonNummer = 1235; // Example of an invalid Swedish personnummer
            bool result = invalidPersonNummer.IsValidPersonNummer();
            result.Should().BeFalse();
        }

#if NET7_0_OR_GREATER
        [TestMethod]
        public void IsValidPersonNummer_Int128_Valid()
        {
            Int128 validPersonNummer = 198112289874; // Example of a valid Swedish personnummer
            bool result = validPersonNummer.IsValidPersonNummer();
            result.Should().BeTrue();
        }

        [TestMethod]
        public void IsValidPersonNummer_Int128_Invalid()
        {
            Int128 invalidPersonNummer = 198501011235; // Example of an invalid Swedish personnummer
            bool result = invalidPersonNummer.IsValidPersonNummer();
            result.Should().BeFalse();
        }
#endif
    }
}

