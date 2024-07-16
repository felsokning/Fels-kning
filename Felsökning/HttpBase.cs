// ----------------------------------------------------------------------
// <copyright file="HttpBase.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="HttpBase"/> class.
    /// </summary>
    /// <inheret doc="IDisposable" />
    public class HttpBase : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpBase"/> class.
        /// </summary>
        /// <param name="httpVersion">The <see cref="HttpVersion"/> the <see cref="System.Net.Http.HttpClient"/> should used.</param>
        /// <param name="productInfoString">The <see cref="OptionalAttribute"/> assembly reference/name to include in the header.</param>
        [MethodImplAttribute(MethodImplOptions.NoInlining)]
        public HttpBase(Version httpVersion, [Optional] string productInfoString)
        {
            var httpClientHandler = new HttpClientHandler()
            {
                // Met Éireann and Ireland West currently don't support TLS 1.3,
                // which should make them feel bad - because it gives me a sad. :(
                SslProtocols = SslProtocols.Tls13 | SslProtocols.Tls12,
            };

            HttpClient = new HttpClient(httpClientHandler)
            {
                // Disallow protocol downgrade[s] - which is a common attack vector.
                DefaultRequestVersion = httpVersion,
                DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher,
            };

            var correlationId = Guid.NewGuid().ToString();
            HttpClient.AddHeader("X-Correlation-ID", correlationId);
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(productInfoString)) 
            {
                var callingAssemblyVersion = Assembly.GetCallingAssembly().GetName().Version;
                if (callingAssemblyVersion != null)
                {
                    HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(Uri.EscapeDataString(productInfoString), callingAssemblyVersion.ToString()));
                }
            }

            HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Contact", Uri.EscapeDataString("nuget@felsokning.se")));
            HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Website", Uri.EscapeDataString("https://www.nuget.org/profiles/felsokning")));
        }

        /// <summary>
        ///     Used to detect redundant calls of disposing.
        /// </summary>
        internal bool disposed;

        /// <summary>
        ///     Gets or sets a singleton instance of <see cref="System.Net.Http.HttpClient"/> used by the class.
        /// </summary>
        public HttpClient HttpClient { get; set; }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates if disposing was called.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    HttpClient.DefaultRequestHeaders.Clear();
                    HttpClient.Dispose();
                }

                disposed = true;
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}