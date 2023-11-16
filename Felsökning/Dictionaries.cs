//-----------------------------------------------------------------------
// <copyright file="Dictionaries.cs" company="Felsökning">
//     Copyright (c) Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
//-----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Dictionaries"/> class,
    ///     which is used internally to processing Swedish Zip Codes.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal sealed class Dictionaries : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Dictionaries"/> class.
        /// </summary>
        internal Dictionaries()
        {
            PostOrt = new Dictionary<int, string>
            {
                // Digits less than 10 are not allowed for city codes.
                { 10, "Stockholm" },
                { 11, "Stockholm" },
                { 12, "Södra Stor Stockholm" },
                { 13, "Södra Stor Stockholm" },
                { 14, "Södra Stor Stockholm" },
                { 15, "Södra Stor Stockholm" },
                { 16, "Norra Stor Stockholm" },
                { 17, "Norra Stor Stockholm" },
                { 18, "Norra Stor Stockholm" },
                { 19, "Norra Stor Stockholm" },
                { 20, "Malmö Box- och företagsadresser" },
                { 21, "Malmö Gatuadresser" },
                { 22, "Lund" },
                { 23, "Sydvästra Skåne län" },
                { 24, "Centrala Skåne län" },
                { 25, "Helsingborg" },
                { 26, "Nordvästra Skåne län" },
                { 27, "Sydöstra Skåne län" },
                { 28, "Norra Skåne och Sydvästra Kronobergs län" },
                { 29, "Nordöstra Skåne Och Västra Blekinge län" },
                { 30, "Halmstad län" },
                { 31, "Södra Hallands län" },
                { 33, "Västra Jönköpings län" },
                { 34, "Västra Kronobergs län" },
                { 35, "Växjö" },
                { 36, "Östra Kronobergs län" },
                { 37, "Mellersta och Östra Blekinge län" },
                { 38, "Södra Kalmar län" },
                { 39, "Kalmar" },
                { 40, "Göteborg Box- och företagsadresser" },
                { 41, "Göteborg Gatuadresser" },
                { 42, "Norra Hallands Och Västra Götalands län" },
                { 43, "Norra Hallands Och Västra Götalands län" },
                { 44, "Norra Hallands Och Västra Götalands län" },
                { 45, "Norra Hallands Och Västra Götalands län" },
                { 46, "Norra Hallands Och Västra Götalands län" },
                { 47, "Norra Hallands Och Västra Götalands län" },
                { 50, "Borås" },
                { 51, "Södra Västra Götalands län" },
                { 52, "ÖstraVästraGötalands län" },
                { 53, "ÖstraVästraGötalands län" },
                { 54, "ÖstraVästraGötalands län" },
                { 55, "Jönköping" },
                { 56, "Norra Jönköpings och Mellersta Kalmar län" },
                { 57, "Norra Jönköpings och Mellersta Kalmar län" },
                { 58, "Linköping" },
                { 59, "Södra Östergötlands och Norra Kalmar län" },
                { 60, "Norrköping" },
                { 61, "Norra Östergötlands och Södra Södermanlands län" },
                { 62, "Gotlands län" },
                { 63, "Eskilstuna" },
                { 64, "Norra Södermanlands län" },
                { 65, "Karlstad " },
                { 66, "Norra Västra Götalands och Värmlands län" },
                { 67, "Norra Västra Götalands och Värmlands län" },
                { 68, "Norra Västra Götalands och Värmlands län" },
                { 69, "Södra Örebro län" },
                { 70, "Örebro" },
                { 71, "Norra Örebro län" },
                { 72, "Västerås" },
                { 73, "Västmanlands län" },
                { 74, "Mellersta och Södra Uppsala län" },
                { 75, "Uppsala" },
                { 76, "Norra Stockholms län" },
                { 77, "Södra Dalarnas län" },
                { 78, "Centrala och Västra Dalarnas län" },
                { 79, "Norra Dalarnas län" },
                { 80, "Gävle" },
                { 81, "Södra Gäveleborgs och Norra Uppsala län" },
                { 82, "Norra Gävlebors län" },
                { 83, "Norra Jämtlands län" },
                { 84, "Södra Jämtlands och Sydvästra Västernorrlands län" },
                { 85, "Sundsvall" },
                { 86, "Sydöstra Västernorrlands län" },
                { 87, "Mellersta Västernorrlands län" },
                { 88, "Nordvästra Västernorrlands län" },
                { 89, "Nordöstra Västernorrlands län" },
                { 90, "Umeå" },
                { 91, "Södra Västerbottens län" },
                { 92, "Mellersta Västerbottens län" },
                { 93, "Nordöstra Västerbottens och Sydvästra Norrbottens län" },
                { 94, "Södra Norrbottens län" },
                { 95, "Mellersta Norrbottens län" },
                { 96, "Sydöstra Norrbottens län" },
                { 97, "Luleå (Före revisionen 951 xx )" },
                { 98, "NorraNorbottens län" },
            };

            Tresifferidentifierade = new Dictionary<int, string>
            {
                { 00, "Box, postala enheter" },
                { 01, "Box, postala enheter" },
                { 02, "Box, postala enheter" },
                { 03, "Box, postala enheter" },
                { 04, "Box, postala enheter" },
                { 05, "Box, postala enheter" },
                { 06, "Box, postala enheter" },
                { 07, "Box, postala enheter" },
                { 08, "Box, postala enheter" },
                { 09, "Box, postala enheter" },
                { 10, "Box, postala enheter" },
                { 11, "Box, postala enheter" },
                { 12, "Box, postala enheter" },
                { 13, "Box, postala enheter" },
                { 14, "Box, postala enheter" },
                { 15, "Box, postala enheter" },
                { 16, "Box, postala enheter" },
                { 17, "Box, postala enheter" },
                { 18, "Box, postala enheter" },
                { 19, "Box, postala enheter" },
                { 20, "Svarspost" },
                { 21, "Boxpost" },
                { 22, "Boxpost" },
                { 23, "Boxpost" },
                { 24, "Boxpost" },
                { 25, "Boxpost" },
                { 26, "Boxpost" },
                { 27, "Boxpost" },
                { 28, "Boxpost" },
                { 29, "Boxpost" },
                { 30, "Utdelningspost tätort" },
                { 31, "Utdelningspost tätort" },
                { 32, "Utdelningspost tätort" },
                { 33, "Utdelningspost tätort" },
                { 34, "Utdelningspost tätort" },
                { 35, "Utdelningspost tätort" },
                { 36, "Utdelningspost tätort" },
                { 37, "Utdelningspost tätort" },
                { 38, "Utdelningspost tätort" },
                { 39, "Utdelningspost tätort" },
                { 40, "Utdelningspost tätort" },
                { 41, "Utdelningspost tätort" },
                { 42, "Utdelningspost tätort" },
                { 43, "Utdelningspost tätort" },
                { 44, "Utdelningspost tätort" },
                { 45, "Utdelningspost tätort" },
                { 46, "Utdelningspost tätort" },
                { 47, "Utdelningspost tätort" },
                { 48, "Utdelningspost tätort" },
                { 49, "Utdelningspost tätort" },
                { 50, "Utdelningspost tätort" },
                { 51, "Utdelningspost tätort" },
                { 52, "Utdelningspost tätort" },
                { 53, "Utdelningspost tätort" },
                { 54, "Utdelningspost tätort" },
                { 55, "Utdelningspost tätort" },
                { 56, "Utdelningspost tätort" },
                { 57, "Utdelningspost tätort" },
                { 58, "Utdelningspost tätort" },
                { 59, "Utdelningspost tätort" },
                { 60, "Utdelningspost tätort" },
                { 61, "Utdelningspost tätort" },
                { 62, "Utdelningspost tätort" },
                { 63, "Utdelningspost tätort" },
                { 64, "Utdelningspost tätort" },
                { 65, "Utdelningspost tätort" },
                { 66, "Utdelningspost tätort" },
                { 67, "Utdelningspost tätort" },
                { 68, "Utdelningspost tätort" },
                { 69, "Utdelningspost tätort" },
                { 70, "Utdelningspost tätort" },
                { 71, "Utdelningspost tätort" },
                { 72, "Utdelningspost tätort" },
                { 73, "Utdelningspost tätort" },
                { 74, "Utdelningspost tätort" },
                { 75, "Utdelningspost tätort" },
                { 76, "Utdelningspost tätort" },
                { 77, "Utdelningspost tätort" },
                { 78, "Utdelningspost tätort" },
                { 79, "Utdelningspost tätort" },
                { 80, "Svarspost/Frisvar" },
                { 81, "Svarspost/Frisvar" },
                { 82, "Svarspost/Frisvar" },
                { 83, "Svarspost/Frisvar" },
                { 84, "Svarspost/Frisvar" },
                { 85, "Svarspost/Frisvar" },
                { 86, "Svarspost/Frisvar" },
                { 87, "Svarspost/Frisvar" },
                { 88, "Svarspost/Frisvar" },
                { 89, "Svarspost/Frisvar" },
                { 90, "Tävlingspost" },
                { 91, "Tävlingspost" },
                { 92, "Tävlingspost" },
                { 93, "Tävlingspost" },
                { 94, "Tävlingspost" },
                { 95, "Tävlingspost" },
                { 96, "Tävlingspost" },
                { 97, "Tävlingspost" },
                { 98, "Tävlingspost" },
                { 99, "Tävlingspost" },
            };

            Utdelningsform = new Dictionary<int, string>
            {
                { 0, "Boxar och Postala Adresser" },
                { 1, "Boxar Företagsadresser" },
                { 2, "Brevbäring" },
                { 3, "Brevbäring" },
                { 4, "Brevbäring" },
                { 5, "Lantbrevbäring" },
                { 6, "Brevbäring" },
                { 7, "Brevbäring" },
                { 8, "Svarspost" },
                { 9, "Tävlingspost" },
            };
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="Dictionaries"/> class.
        /// </summary>
        ~Dictionaries()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Gets or sets a dictionary used to reference city codes in the Swedish Postnummer.
        /// </summary>
        internal static Dictionary<int, string> PostOrt { get; set; } = new Dictionary<int, string>();

        /// <summary>
        ///     Gets or sets a dictionary used to reference the delivery form in the Swedish Postnummer.
        /// </summary>
        internal static Dictionary<int, string> Utdelningsform { get; set; } = new Dictionary<int, string>();

        /// <summary>
        ///     Gets or sets a dictionary used to reference the delivery type in the Swedish Postnummer.
        /// </summary>
        internal static Dictionary<int, string> Tresifferidentifierade { get; set; } = new Dictionary<int, string>();

        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates if disposing was called.</param>
        public void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    PostOrt.Clear();
                    Tresifferidentifierade.Clear();
                    Utdelningsform.Clear();
                }

                PostOrt.Clear();
                Tresifferidentifierade.Clear();
                Utdelningsform.Clear();

                disposedValue = true;
            }
        }
    }
}
