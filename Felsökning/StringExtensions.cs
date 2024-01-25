// ----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StringExtensions"/> class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Extends the <see cref="string"/> object to try to return details for a given Swedish postnummer.
        /// </summary>
        /// <param name="value">The current string context.</param>
        /// <returns>A string containing details about the postnummer.</returns>
        public static string GetPostnummerDetails(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            // Because postnummers are often written as "nnn nn", we need to sanitize the input.
            if (value.Contains(value: " "))
            {
                value = Regex.Replace(input: value, pattern: " ", replacement: string.Empty);
            }

            // Don't bother going forward if the string is too short.
            if (value.Length < 5)
            {
                throw new ArgumentException(message: "The parameter supplied was too short to be a valid Swedish postnummer.");
            }

            // Don't bother going forward if the string is too long.
            if (value.Length > 5)
            {
                throw new ArgumentException(message: "The parameter supplied was too long to be a valid Swedish postnummer.");
            }

            // Throw if we have any non-numeric characters
            if (!int.TryParse(s: value, out _))
            {
                throw new ArgumentException(message: "The parameter supplied had non-numeric characters, which postnummers do not.");
            }

            List<string> returns = new List<string>(0);

            using (Dictionaries dictionaries = new Dictionaries())
            {
                string cityCodeString = value.Substring(startIndex: 0, length: 2);
                string utdelningsformString = value.Substring(startIndex: 3, length: 1);
                string tresifferidentifieradString = value.Substring(startIndex: 3, 2);
                int cityCode = int.Parse(s: cityCodeString);
                int utdelningsform = int.Parse(s: utdelningsformString);
                int tresifferidentifierad = int.Parse(s: tresifferidentifieradString);
                returns.Add(item: Dictionaries.PostOrt[cityCode]);
                returns.Add(item: Dictionaries.Utdelningsform[utdelningsform]);
                returns.Add(item: Dictionaries.Tresifferidentifierade[tresifferidentifierad]);
            }

            return returns.ToArray().ToArrayString();
        }

        /// <summary>
        ///     Extends the <see cref="string"/> class to include validation if the given string is a valid Swedish personnummer via the Luhn Algorithm.
        /// </summary>
        /// <param name="value">The current string value context.</param>
        /// <returns>A boolean indicating if the checksum values match.</returns>
        public static bool IsValidSwedishPersonNummer(this string value)
        {
            if (string.IsNullOrWhiteSpace(value: value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            bool isSizedTen = false;
            bool isSizedTwelve = false;

            // Two Year: YYMMDD-SSSC
            bool minusCharTwoDigitYear = value[6] == '-';
            bool plusCharTwoDigitYear = value[6] == '+';

            // Four Year: YYYYMMDD-SSSC
            bool minusCharFourDigitYear = value[8] == '-';
            bool plusCharFourDigitYear = value[8] == '+';

            if (minusCharTwoDigitYear || plusCharTwoDigitYear || minusCharFourDigitYear || plusCharFourDigitYear)
            {
                // Clean-up before next step
                if (value.Contains(value: "-"))
                {
                    string pattern = "-";
                    value = Regex.Replace(input: value, pattern: pattern, replacement: string.Empty);
                }

                if (value.Contains(value: "+"))
                {
                    string pattern = "\\+";
                    value = Regex.Replace(input: value, pattern: pattern, replacement: string.Empty);
                }
            }

            if (value.Length == 10)
            {
                isSizedTen = true;
            }

            if (value.Length == 12)
            {
                isSizedTwelve = true;
            }

            if (!isSizedTen && !isSizedTwelve)
            {
                throw new ArgumentException($"String is incorrect size: {value.Length}");
            }

            // Validate after clean-up that we have numbers.
            if (!long.TryParse(s: value, result: out _))
            {
                throw new ArgumentException($"Unable to parse '{value}' to long.");
            }

            int first = 0;
            int second = 0;
            int third = 0;
            int fourth = 0;
            int fifth = 0;
            int sixth = 0;
            int seventh = 0;
            int eighth = 0;
            int ninth = 0;
            int tenth = 0;

            // Luhn Algorithm Magics - See: https://www.ncbi.nlm.nih.gov/pmc/articles/PMC2773709/figure/Fig1/
            if (isSizedTen)
            {
                first = int.Parse(value.Substring(startIndex: 0, length: 1));       // Year
                second = int.Parse(value.Substring(startIndex: 1, length: 1));      // Year
                third = int.Parse(value.Substring(startIndex: 2, length: 1));       // Month
                fourth = int.Parse(value.Substring(startIndex: 3, length: 1));      // Month
                fifth = int.Parse(value.Substring(startIndex: 4, length: 1));       // Day
                sixth = int.Parse(value.Substring(startIndex: 5, length: 1));       // Day
                seventh = int.Parse(value.Substring(startIndex: 6, length: 1));     // Serial
                eighth = int.Parse(value.Substring(startIndex: 7, length: 1));      // Serial
                ninth = int.Parse(value.Substring(startIndex: 8, length: 1));       // Sex
                tenth = int.Parse(value.Substring(startIndex: 9, length: 1));       // Checksum
            }
            else if (isSizedTwelve)
            {
                first = int.Parse(value.Substring(startIndex: 2, length: 1));       // Year
                second = int.Parse(value.Substring(startIndex: 3, length: 1));      // Year
                third = int.Parse(value.Substring(startIndex: 4, length: 1));       // Month
                fourth = int.Parse(value.Substring(startIndex: 5, length: 1));      // Month
                fifth = int.Parse(value.Substring(startIndex: 6, length: 1));       // Day
                sixth = int.Parse(value.Substring(startIndex: 7, length: 1));       // Day
                seventh = int.Parse(value.Substring(startIndex: 8, length: 1));     // Serial
                eighth = int.Parse(value.Substring(startIndex: 9, length: 1));      // Serial
                ninth = int.Parse(value.Substring(startIndex: 10, length: 1));      // Sex
                tenth = int.Parse(value.Substring(startIndex: 11, length: 1));      // Checksum
            }

            int firstProduct = first * 2;
            int secondProduct = second * 1;
            int thirdProduct = third * 2;
            int fourthProduct = fourth * 1;
            int fifthProduct = fifth * 2;
            int sixthProduct = sixth * 1;
            int seventhProduct = seventh * 2;
            int eighthProduct = eighth * 1;
            int ninthProduct = ninth * 2;

            if (firstProduct > 9)
            {
                firstProduct = ReduceGreaterThanNine(product: firstProduct);
            }

            if (thirdProduct > 9)
            {
                thirdProduct = ReduceGreaterThanNine(product: thirdProduct);
            }

            if (fifthProduct > 9)
            {
                fifthProduct = ReduceGreaterThanNine(product: fifthProduct);
            }

            if (seventhProduct > 9)
            {
                seventhProduct = ReduceGreaterThanNine(product: seventhProduct);
            }

            if (ninthProduct > 9)
            {
                ninthProduct = ReduceGreaterThanNine(product: ninthProduct);
            }

            int sumTotal = firstProduct + secondProduct + thirdProduct + fourthProduct + fifthProduct + sixthProduct + seventhProduct + eighthProduct + ninthProduct;
            string sumTotalString = sumTotal.ToString();
            int sumTotalStringLength = sumTotalString.Length;
            string lastdigit = sumTotalString.Substring(startIndex: sumTotalStringLength - 1, length: 1);
            int lastActualDigit = int.Parse(s: lastdigit);
            int checksum = 0;

            // Ten minus zero is ten and, so, the last digit would be zero, anyways.
            if (lastActualDigit != 0)
            {
                checksum = 10 - lastActualDigit;
            }

            return tenth == checksum;
        }

        /// <summary>
        ///     Extends the <see cref="T:string[]"/> object to return a comma-separated string of the items in the string array.
        /// </summary>
        /// <param name="value">The current <see cref="T:string[]"/> object.</param>
        /// <returns>A string containing the string array's contents, comma-separated.</returns>
        public static string ToArrayString(this string[] value)
        {
            if (value == null || value.Length == 0)
            {
                throw new StatusException(
                    "The given string was either null, empty, or whitespace",
                    new ArgumentNullException(nameof(value)));
            }

            StringBuilder stringBuilder = new StringBuilder();
            for (int c = 0; c <= value.Length - 1; c++)
            {
                if (c == value.Length - 1)
                {
                    stringBuilder.Append(value: value[c]);
                }
                else
                {
                    stringBuilder.Append(value: value[c] + ", ");
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Validates the given string is not null, empty, or whitespace.
        /// </summary>
        /// <param name="value">The current string value context.</param>
        public static void Validate(this string value)
        {
            if (string.IsNullOrWhiteSpace(value: value))
            {
                throw new StatusException(
                    "The given string was either null, empty, or whitespace",
                    new ArgumentException($"The given string was either null, empty, or whitespace", nameof(value)));
            }
        }

        /// <summary>
        ///     Validates the given string is not null, empty, or whitespace.
        ///     Also validates that the given string is a minimum length, as expected.
        /// </summary>
        /// <param name="value">The current string value context.</param>
        /// <param name="length">The expected length of the string.</param>
        public static void Validate(this string value, int length)
        {
            if (string.IsNullOrWhiteSpace(value: value))
            {
                throw new StatusException(
                    "The given string was either null, empty, or whitespace",
                    new ArgumentException("The given string was either null, empty, or whitespace", nameof(value)));
            }

            if (value.Length != length)
            {
                throw new StatusException(
                    $"The given string was not the expected length of {length}",
                    new ArgumentException($"The given string was not the expected length of {length}", nameof(value)));
            }
        }

        /// <summary>
        ///     Returns the sum of a product, when the product is greater than nine.
        /// </summary>
        /// <param name="product">The product to be handled.</param>
        /// <returns>The sum of the product's integers.</returns>
        [ExcludeFromCodeCoverage]
        private static int ReduceGreaterThanNine(int product)
        {
            if (product > 9)
            {
                string productString = product.ToString();
                int returnInt = int.Parse(productString.Substring(startIndex: 0, length: 1)) + int.Parse(productString.Substring(startIndex: 1, length: 1));
                return returnInt;
            }

            throw new ArgumentException($"The product {product} was not greater than nine and, thus, did not need to be processed by this method.");
        }
    }
}