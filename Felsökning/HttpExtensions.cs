// ----------------------------------------------------------------------
// <copyright file="HttpExtensions.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
using System.ComponentModel;

namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="HttpExtensions"/> class, 
    ///     which is used to extend Http-Related classes.
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        ///     Adds the given header name and value to the <see cref="HttpClient"/>.
        ///     <para>WARNING: The existing header of the same name will be removed, if it exists.</para>
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="name">The header to add to the collection.</param>
        /// <param name="value">The content of the header.</param>
        public static void AddHeader(this HttpClient httpClient, string name, string value)
        {
            httpClient.RemoveHeader(name);
            httpClient.DefaultRequestHeaders.Add(name: name, value: value);
        }

        /// <summary>
        ///     Adds the given request id to the <see cref="HttpClient"/>.
        ///     <para>WARNING: The existing header of the same name will be removed, if it exists.</para>
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestId"></param>
        public static void AddNewRequestId(this HttpClient httpClient, string requestId)
        {
            httpClient.RemoveHeader("X-Request-ID");
            httpClient.AddHeader("X-Request-ID", requestId);
        }

        /// <summary>
        ///     Generates a new request id for the given <see cref="HttpClient"/> for tracking/tracing reasons.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        public static string GenerateNewRequestId(this HttpClient httpClient)
        {
            var generatedRequestId = Guid.NewGuid().ToString();
            httpClient.AddHeader("X-Request-ID", generatedRequestId);
            return generatedRequestId;
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     <para>We only check for successful HTTP responses. Any continuations must be handled by the caller.</para>
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <returns>An awaitable <see cref="Task{T}"/> of <typeparamref name="T"/></returns>
        public static async Task<T> GetAsync<T>(this HttpClient httpClient, string requestUrl )
        {
            var requestId = string.Empty;
            try
            {
                requestId = httpClient.GenerateNewRequestId();
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(requestUrl);
                string httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options)!;
                }
                else
                {
                    var httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get.Method,
                        StatusCode = httpResponseMessage.StatusCode.ToString(),
                        Content = httpResponseMessageContent,
                    };

                    throw new StatusException(httpResponseMessage.StatusCode.ToString(), httpResponseMessageContent, httpRecord);
                }
            }
            catch (HttpRequestException thrownException)
            {
                HttpRecord? httpRecord;
                if (thrownException.StatusCode == null)
                {
                    httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get.Method,
                        StatusCode = string.Empty,
                        Content = thrownException.Message,
                    };
                }
                else
                {
                    httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get.Method,
                        StatusCode = thrownException.StatusCode!.ToString()!,
                        Content = thrownException.Message,
                    };
                }

                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException, httpRecord);
            }
        }

        /// <summary>
        ///     Deserializes data and sends the patch request to update the object.
        /// </summary>
        /// <typeparam name="T">The base type to be deserialized and patched.</typeparam>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="typeObject">The object to be deserialized and patched.</param>
        /// <returns>An awaitable <see cref="Task{T}"/> of <typeparamref name="T"/></returns>
        public static async Task<T> PatchAsync<T>(this HttpClient httpClient, string requestUrl, T typeObject)
        {
            var requestId = string.Empty;
            try
            {
                requestId = httpClient.GenerateNewRequestId();
                HttpRequestMessage httpRequestMessage = new(method: new HttpMethod("PATCH"), requestUri: requestUrl)
                {
                    Content = new StringContent(content: JsonSerializer.Serialize(typeObject))
                };
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request: httpRequestMessage);
                string httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options)!;
                }
                else
                {
                    var httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Patch.ToString(),
                        StatusCode = httpResponseMessage.StatusCode.ToString(),
                        Content = httpResponseMessageContent,
                    };

                    throw new StatusException($"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", httpResponseMessageContent, httpRecord);
                }
            }
            catch (HttpRequestException thrownException)
            {
                HttpRecord? httpRecord;
                if (thrownException.StatusCode == null)
                {
                    httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get.Method,
                        StatusCode = string.Empty,
                        Content = thrownException.Message,
                    };
                }
                else
                {
                    httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get.Method,
                        StatusCode = thrownException.StatusCode!.ToString()!,
                        Content = thrownException.Message,
                    };
                }

                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException, httpRecord);
            }
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     We only check for successful HTTP responses. Any continuations must be handled by the caller.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="httpContent">The content to be posted, in string form.</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public static async Task<T> PostAsync<T>(this HttpClient httpClient, string requestUrl, HttpContent httpContent)
        {
            var requestId = string.Empty;
            try
            {
                requestId = httpClient.GenerateNewRequestId();
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri: requestUrl, content: httpContent);
                string httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options)!;
                }
                else
                {
                    var httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Post.ToString(),
                        StatusCode = httpResponseMessage.StatusCode.ToString(),
                        Content = httpResponseMessageContent,
                    };

                    throw new StatusException($"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", httpResponseMessageContent, httpRecord);
                }
            }
            catch (HttpRequestException thrownException)
            {
                HttpRecord? httpRecord;
                if (thrownException.StatusCode == null)
                {
                    httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get.Method,
                        StatusCode = string.Empty,
                        Content = thrownException.Message,
                    };
                }
                else
                {
                    httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get.Method,
                        StatusCode = thrownException.StatusCode!.ToString()!,
                        Content = thrownException.Message,
                    };
                }

                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException, httpRecord);
            }
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     We only check for successful HTTP responses. Any continuations must be handled by the caller.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="stringContent">The content to be posted, in string form.</param>
        /// <param name="contentType">The content type the server should be expecting.</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public static async Task<T> PostAsync<T>(this HttpClient httpClient, string requestUrl, string stringContent, string contentType)
        {
            var requestId = string.Empty;
            try
            {
                requestId = httpClient.GenerateNewRequestId();
                HttpContent httpContent = new StringContent(content: stringContent);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType: contentType);
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri: requestUrl, content: httpContent);
                string? httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options)!;
                }
                else
                {
                    var httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Post.ToString(),
                        StatusCode = httpResponseMessage.StatusCode.ToString(),
                        Content = httpResponseMessageContent,
                    };

                    throw new StatusException($"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", httpResponseMessageContent, httpRecord);
                }
            }
            catch (HttpRequestException thrownException)
            {
                HttpRecord? httpRecord;
                if (thrownException.StatusCode == null)
                {
                    httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get.Method,
                        StatusCode = string.Empty,
                        Content = thrownException.Message,
                    };
                }
                else
                {
                    httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get.Method,
                        StatusCode = thrownException.StatusCode!.ToString()!,
                        Content = thrownException.Message,
                    };
                }

                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException, httpRecord);
            }
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     We only check for successful HTTP responses. Any continuations must be handled by the caller.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="httpContent">The content to be put.</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public static async Task<T> PutAsync<T>(this HttpClient httpClient, string requestUrl, HttpContent httpContent)
        {
            var requestId = string.Empty;
            try
            {
                requestId = httpClient.GenerateNewRequestId();
                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(requestUri: requestUrl, content: httpContent);
                string? httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options)!;
                }
                else
                {
                    var httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Post.ToString(),
                        StatusCode = httpResponseMessage.StatusCode.ToString(),
                        Content = httpResponseMessageContent,
                    };

                    throw new StatusException($"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", httpResponseMessageContent, httpRecord);
                }
            }
            catch (HttpRequestException thrownException)
            {
                HttpRecord? httpRecord;
                if (thrownException.StatusCode == null)
                {
                    httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get.Method,
                        StatusCode = string.Empty,
                        Content = thrownException.Message,
                    };
                }
                else
                {
                    httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get.Method,
                        StatusCode = thrownException.StatusCode!.ToString()!,
                        Content = thrownException.Message,
                    };
                }

                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException, httpRecord);
            }
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     We only check for successful HTTP responses. Any continuations must be handled by the caller.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="stringContent">The content to be put, in string form.</param>
        /// <param name="contentType">The content type the server should be expecting.</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public static async Task<T> PutAsync<T>(this HttpClient httpClient, string requestUrl, string stringContent, string contentType)
        {
            var requestId = string.Empty;
            try
            {
                requestId = httpClient.GenerateNewRequestId(); 
                HttpContent httpContent = new StringContent(content: stringContent);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType: contentType);
                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(requestUri: requestUrl, content: httpContent);
                string? httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options)!;
                }
                else
                {
                    var httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Post.ToString(),
                        StatusCode = httpResponseMessage.StatusCode.ToString(),
                        Content = httpResponseMessageContent,
                    };

                    throw new StatusException($"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", httpResponseMessageContent, httpRecord);
                }
            }
            catch (HttpRequestException thrownException)
            {
                HttpRecord? httpRecord;
                if (thrownException.StatusCode == null)
                {
                    httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get.Method,
                        StatusCode = string.Empty,
                        Content = thrownException.Message,
                    };
                }
                else
                {
                    httpRecord = new HttpRecord
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get.Method,
                        StatusCode = thrownException.StatusCode!.ToString()!,
                        Content = thrownException.Message,
                    };
                }

                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException, httpRecord);
            }
        }

        /// <summary>
        ///     Removes the given header, if it exists.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="name">The header to be removed from the <see cref="HttpClient.DefaultRequestHeaders"/> context.</param>
        public static void RemoveHeader(this HttpClient httpClient, string name)
        {
            if (httpClient.DefaultRequestHeaders.Contains(name: name))
            {
                httpClient.DefaultRequestHeaders.Remove(name: name);
            }
        }
    }
}