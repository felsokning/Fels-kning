// ----------------------------------------------------------------------
// <copyright file="TestingHttpMessageHandler.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.Tests
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TestingHttpMessageHandler"/> class,
    ///     which is used to model HTTP Response messages back to the caller, based on URL.
    /// </summary>
    /// <inheritdoc cref="HttpMessageHandler"/>
    [ExcludeFromCodeCoverage]
    internal class TestingHttpMessageHandler : HttpMessageHandler
    {
        /// <summary>
        ///     Overrides the <see cref="SendAsync(HttpRequestMessage, CancellationToken)"/> method in <see cref="HttpMessageHandler"/>
        ///     to return a specified <see cref="HttpResponseMessage"/>, based on the URL called.
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task{TResult}"/> of <see cref="HttpResponseMessage"/> for the test class[es] to consume.</returns>
        /// <exception cref="HttpRequestException">A response to mock exceptions thrown on request.</exception>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();

            if (httpRequestMessage?.RequestUri?.AbsoluteUri == "https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty")
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                responseMessage.StatusCode = HttpStatusCode.OK;
                responseMessage.Content = new StringContent("[ 32649091 ]");

                return Task<HttpResponseMessage>.Factory.StartNew(() => responseMessage, cancellationToken);
            }

            if (httpRequestMessage?.RequestUri?.AbsoluteUri == "https://hacker-news.firebaseio.com/v0/item/32649091.json?print=pretty")
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                responseMessage.StatusCode = HttpStatusCode.OK;
                responseMessage.Content = new StringContent("{\r\n  \"by\" : \"vinnyglennon\",\r\n  \"descendants\" : 36,\r\n  \"id\" : 32649091,\r\n  \"kids\" : [ 32650627, 32652889, 32651364, 32651377, 32655335, 32652375, 32652023, 32649456, 32651630, 32649718, 32653799, 32650386, 32657988, 32650124 ],\r\n  \"score\" : 162,\r\n  \"time\" : 1661859463,\r\n  \"title\" : \"Wikipedia Recent Changes Map\",\r\n  \"type\" : \"story\",\r\n  \"url\" : \"http://rcmap.hatnote.com/#en\"\r\n}");

                return Task<HttpResponseMessage>.Factory.StartNew(() => responseMessage, cancellationToken);
            }

            if ((bool)httpRequestMessage?.RequestUri?.AbsoluteUri?.Contains("https://api.dw.com/api/search/global?terms=*&contentTypes=Article&languageId=15"))
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                responseMessage.StatusCode = HttpStatusCode.OK;
                responseMessage.Content = new StringContent("{\r\n  \"languageId\": 15,\r\n  \"paginationInfo\": {\r\n    \"availableItems\": 7,\r\n    \"availablePages\": 1,\r\n    \"pageSize\": 20,\r\n    \"currentPage\": 1,\r\n    \"itemsOnPage\": 7,\r\n    \"firstItem\": 0,\r\n    \"lastItem\": 6\r\n  },\r\n  \"items\": [\r\n    {\r\n      \"type\": \"BasicTeaser\",\r\n      \"name\": \"Kashedi kan yiwuwar aukuwar fari a Somaliya\",\r\n      \"teaserText\": \"Majalisar Dinkin Duniya ta ce ana dab da fadawa matsalar fari a Somaliya a bana musamman a yankin kudancin kasar. \",\r\n      \"displayDate\": \"2022-09-05T19:30:50.595Z\",\r\n      \"image\": {\r\n        \"id\": 63022366,\r\n        \"type\": \"Image\",\r\n        \"name\": \"Hungerkrise in Somalia\",\r\n        \"sizes\": [\r\n          {\r\n            \"width\": 1024,\r\n            \"height\": 448,\r\n            \"url\": \"https://static.dw.com/image/63022366_103.jpg\"\r\n          }\r\n        ]\r\n      },\r\n      \"reference\": {\r\n        \"id\": 63026167,\r\n        \"type\": \"ArticleRef\",\r\n        \"name\": \"MDD ta yi gargadin aukuwar fari a Somaliya\",\r\n        \"url\": \"https://api.dw.com/api/detail/article/63026167\"\r\n      },\r\n      \"columnCount\": 1,\r\n      \"allowedColumnCounts\": [\r\n        1,\r\n        2\r\n      ],\r\n      \"commentsEnabled\": false\r\n    },\r\n    {\r\n      \"type\": \"BasicTeaser\",\r\n      \"name\": \"An zabi Truss sabuwar Fraministar Birtaniya\",\r\n      \"teaserText\": \"An zabi sabuwar Firaminista a Birtaniya\",\r\n      \"displayDate\": \"2022-09-05T19:21:51.526Z\",\r\n      \"image\": {\r\n        \"id\": 63021722,\r\n        \"type\": \"Image\",\r\n        \"name\": \"Liz Truss wird neue britische Premierministerin\",\r\n        \"sizes\": [\r\n          {\r\n            \"width\": 220,\r\n            \"height\": 124,\r\n            \"url\": \"https://static.dw.com/image/63021722_301.jpg\"\r\n          },\r\n          {\r\n            \"width\": 460,\r\n            \"height\": 259,\r\n            \"url\": \"https://static.dw.com/image/63021722_302.jpg\"\r\n          },\r\n          {\r\n            \"width\": 700,\r\n            \"height\": 394,\r\n            \"url\": \"https://static.dw.com/image/63021722_303.jpg\"\r\n          },\r\n          {\r\n            \"width\": 940,\r\n            \"height\": 529,\r\n            \"url\": \"https://static.dw.com/image/63021722_304.jpg\"\r\n          }\r\n        ]\r\n      },\r\n      \"reference\": {\r\n        \"id\": 63024352,\r\n        \"type\": \"ArticleRef\",\r\n        \"name\": \"Truss: Sabuwar Firaministar Birtaniya\",\r\n        \"url\": \"https://api.dw.com/api/detail/article/63024352\"\r\n      },\r\n      \"columnCount\": 1,\r\n      \"allowedColumnCounts\": [\r\n        1,\r\n        2\r\n      ],\r\n      \"commentsEnabled\": false\r\n    },\r\n    {\r\n      \"type\": \"BasicTeaser\",\r\n      \"name\": \"Shekaru 50 na jimamin kisan 'yan wasan Olympics a Munich\",\r\n      \"teaserText\": \"Jimamin kisan 'yan wasan Israila a gasar Olympics a birnin Munich\",\r\n      \"displayDate\": \"2022-09-05T19:18:55.924Z\",\r\n      \"image\": {\r\n        \"id\": 63023504,\r\n        \"type\": \"Image\",\r\n        \"name\": \"Gedenkveranstaltung zum 50. Jahrestag des Olympiaattentats\",\r\n        \"sizes\": [\r\n          {\r\n            \"width\": 220,\r\n            \"height\": 124,\r\n            \"url\": \"https://static.dw.com/image/63023504_301.jpg\"\r\n          },\r\n          {\r\n            \"width\": 460,\r\n            \"height\": 259,\r\n            \"url\": \"https://static.dw.com/image/63023504_302.jpg\"\r\n          },\r\n          {\r\n            \"width\": 700,\r\n            \"height\": 394,\r\n            \"url\": \"https://static.dw.com/image/63023504_303.jpg\"\r\n          },\r\n          {\r\n            \"width\": 940,\r\n            \"height\": 529,\r\n            \"url\": \"https://static.dw.com/image/63023504_304.jpg\"\r\n          }\r\n        ]\r\n      },\r\n      \"reference\": {\r\n        \"id\": 63026142,\r\n        \"type\": \"ArticleRef\",\r\n        \"name\": \"Shekaru 50 na jimamin kisan 'yan wasan Olympics na Israila a Munich\",\r\n        \"url\": \"https://api.dw.com/api/detail/article/63026142\"\r\n      },\r\n      \"columnCount\": 1,\r\n      \"allowedColumnCounts\": [\r\n        1,\r\n        2\r\n      ],\r\n      \"commentsEnabled\": false\r\n    },\r\n    {\r\n      \"type\": \"BasicTeaser\",\r\n      \"name\": \"Serena Williams ta yi bankwana da tennis\",\r\n      \"teaserText\": \"Fitacciyar 'yar wasan Tennis ta duniya Serena Williams ta yi bankwana da wasan, za kuma mu leka wasannin lig na Turai.\",\r\n      \"displayDate\": \"2022-09-05T17:23:35.560Z\",\r\n      \"image\": {\r\n        \"id\": 60525246,\r\n        \"type\": \"Image\",\r\n        \"name\": \"Deutschland Bundesliga | TSG 1899 Hoffenheim vs Borussia Dortmund | Tor (1:2)\",\r\n        \"sizes\": [\r\n          {\r\n            \"width\": 220,\r\n            \"height\": 124,\r\n            \"url\": \"https://static.dw.com/image/60525246_301.jpg\"\r\n          },\r\n          {\r\n            \"width\": 460,\r\n            \"height\": 259,\r\n            \"url\": \"https://static.dw.com/image/60525246_302.jpg\"\r\n          },\r\n          {\r\n            \"width\": 700,\r\n            \"height\": 394,\r\n            \"url\": \"https://static.dw.com/image/60525246_303.jpg\"\r\n          },\r\n          {\r\n            \"width\": 940,\r\n            \"height\": 529,\r\n            \"url\": \"https://static.dw.com/image/60525246_304.jpg\"\r\n          }\r\n        ]\r\n      },\r\n      \"reference\": {\r\n        \"id\": 63025412,\r\n        \"type\": \"ArticleRef\",\r\n        \"name\": \"Labarin Wasanni: Bankwanan Serena Williams\",\r\n        \"url\": \"https://api.dw.com/api/detail/article/63025412\"\r\n      },\r\n      \"columnCount\": 1,\r\n      \"allowedColumnCounts\": [\r\n        1,\r\n        2\r\n      ],\r\n      \"commentsEnabled\": false\r\n    },\r\n    {\r\n      \"type\": \"BasicTeaser\",\r\n      \"name\": \"Najeriya na barar a yafe mata basussuka\",\r\n      \"teaserText\": \"Gwamnatin Najeriya na neman lamuni na bashin da ake bin ta a ketare, domin tunkarar matsalar sauyin yanayi.\",\r\n      \"displayDate\": \"2022-09-05T17:00:34.412Z\",\r\n      \"image\": {\r\n        \"id\": 44705827,\r\n        \"type\": \"Image\",\r\n        \"name\": \"Katsina Flut Nigeria\",\r\n        \"sizes\": [\r\n          {\r\n            \"width\": 220,\r\n            \"height\": 124,\r\n            \"url\": \"https://static.dw.com/image/44705827_301.jpg\"\r\n          },\r\n          {\r\n            \"width\": 460,\r\n            \"height\": 259,\r\n            \"url\": \"https://static.dw.com/image/44705827_302.jpg\"\r\n          },\r\n          {\r\n            \"width\": 700,\r\n            \"height\": 394,\r\n            \"url\": \"https://static.dw.com/image/44705827_303.jpg\"\r\n          },\r\n          {\r\n            \"width\": 940,\r\n            \"height\": 529,\r\n            \"url\": \"https://static.dw.com/image/44705827_304.jpg\"\r\n          }\r\n        ]\r\n      },\r\n      \"reference\": {\r\n        \"id\": 63024403,\r\n        \"type\": \"ArticleRef\",\r\n        \"name\": \"Sauyin yanyi: Najeriya na maula kan bashi\",\r\n        \"url\": \"https://api.dw.com/api/detail/article/63024403\"\r\n      },\r\n      \"columnCount\": 1,\r\n      \"allowedColumnCounts\": [\r\n        1,\r\n        2\r\n      ],\r\n      \"commentsEnabled\": false\r\n    },\r\n    {\r\n      \"type\": \"BasicTeaser\",\r\n      \"name\": \"Jamus: Shekaru 50 da harin Olympics\",\r\n      \"teaserText\": \"Shekaru 27 bayan yakin duniya na biyu an kai waYahudawa hari a Jamus, inda aka halaka 11 da dan sanda daya.\",\r\n      \"displayDate\": \"2022-09-05T16:34:43.641Z\",\r\n      \"image\": {\r\n        \"id\": 58323303,\r\n        \"type\": \"Image\",\r\n        \"name\": \"Deutschland, München | Trauer nach Münchner OEZ-Attentat\",\r\n        \"sizes\": [\r\n          {\r\n            \"width\": 220,\r\n            \"height\": 124,\r\n            \"url\": \"https://static.dw.com/image/58323303_301.jpg\"\r\n          },\r\n          {\r\n            \"width\": 460,\r\n            \"height\": 259,\r\n            \"url\": \"https://static.dw.com/image/58323303_302.jpg\"\r\n          },\r\n          {\r\n            \"width\": 700,\r\n            \"height\": 394,\r\n            \"url\": \"https://static.dw.com/image/58323303_303.jpg\"\r\n          },\r\n          {\r\n            \"width\": 940,\r\n            \"height\": 529,\r\n            \"url\": \"https://static.dw.com/image/58323303_304.jpg\"\r\n          }\r\n        ]\r\n      },\r\n      \"reference\": {\r\n        \"id\": 63025025,\r\n        \"type\": \"ArticleRef\",\r\n        \"name\": \"Shekaru 50 da kisan Yahudawa a Jamus\",\r\n        \"url\": \"https://api.dw.com/api/detail/article/63025025\"\r\n      },\r\n      \"columnCount\": 1,\r\n      \"allowedColumnCounts\": [\r\n        1,\r\n        2\r\n      ],\r\n      \"commentsEnabled\": false\r\n    },\r\n    {\r\n      \"type\": \"BasicTeaser\",\r\n      \"name\": \"Jamus ta ware biliyoyi don rage radadin rayuwa\",\r\n      \"teaserText\": \"Jamus ta shirya amfani da kudi Euro biliyan 65 domin rage wa iyalai da kamfanonin radadin tsadar makamashi.\",\r\n      \"displayDate\": \"2022-09-05T07:37:43.126Z\",\r\n      \"image\": {\r\n        \"id\": 63014605,\r\n        \"type\": \"Image\",\r\n        \"name\": \"Deutschland Berlin | PK Koalitionsausschuss zum Entlastungspaket\",\r\n        \"sizes\": [\r\n          {\r\n            \"width\": 220,\r\n            \"height\": 124,\r\n            \"url\": \"https://static.dw.com/image/63014605_301.jpg\"\r\n          },\r\n          {\r\n            \"width\": 460,\r\n            \"height\": 259,\r\n            \"url\": \"https://static.dw.com/image/63014605_302.jpg\"\r\n          },\r\n          {\r\n            \"width\": 700,\r\n            \"height\": 394,\r\n            \"url\": \"https://static.dw.com/image/63014605_303.jpg\"\r\n          },\r\n          {\r\n            \"width\": 940,\r\n            \"height\": 529,\r\n            \"url\": \"https://static.dw.com/image/63014605_304.jpg\"\r\n          }\r\n        ]\r\n      },\r\n      \"reference\": {\r\n        \"id\": 63018480,\r\n        \"type\": \"ArticleRef\",\r\n        \"name\": \" Jamus ta ware biliyoyin Euro don rage radadin tsadar rayuwa\",\r\n        \"url\": \"https://api.dw.com/api/detail/article/63018480\"\r\n      },\r\n      \"columnCount\": 1,\r\n      \"allowedColumnCounts\": [\r\n        1,\r\n        2\r\n      ],\r\n      \"commentsEnabled\": false\r\n    }\r\n  ],\r\n  \"trackingInfo\": {\r\n    \"level2\": \"15\",\r\n    \"page\": \"<prefix>::Bincike::Bincike\",\r\n    \"customCriteria\": {\r\n      \"x8\": \"\",\r\n      \"x9\": \"20220906\",\r\n      \"x10\": \"<prefix>::Bincike\",\r\n      \"x1\": \"101\",\r\n      \"x2\": \"15\",\r\n      \"X14\": \"\",\r\n      \"x3\": \"627\",\r\n      \"x4\": \"627\",\r\n      \"x5\": \"Bincike\",\r\n      \"X15\": \"\",\r\n      \"x6\": \"1\",\r\n      \"X18\": \"\",\r\n      \"x7\": \"\"\r\n    }\r\n  },\r\n  \"resultCount\": 7,\r\n  \"filterParameters\": {\r\n    \"terms\": \"*\",\r\n    \"startDate\": \"2022-09-05T00:00:00.000Z\",\r\n    \"endDate\": \"2022-09-06T00:00:00.000Z\",\r\n    \"sortByDate\": true,\r\n    \"contentTypes\": [\r\n      \"Article\"\r\n    ],\r\n    \"programIds\": [],\r\n    \"categoryIds\": [],\r\n    \"contentIds\": []\r\n  }\r\n}");

                return Task<HttpResponseMessage>.Factory.StartNew(() => responseMessage, cancellationToken);
            }

            if (httpRequestMessage?.RequestUri?.AbsoluteUri == "https://jsonplaceholder.typicode.com/todos/1")
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                responseMessage.StatusCode = HttpStatusCode.OK;
                responseMessage.Content = new StringContent(JsonSerializer.Serialize(new SampleJson
                {
                    Completed = true,
                    Id = 8675309,
                    Title = "Super Secret and Diabolical Plans",
                    UserId = 24
                }));

                return Task<HttpResponseMessage>.Factory.StartNew(() => responseMessage, cancellationToken);
            }

            if (httpRequestMessage?.RequestUri?.AbsoluteUri == "https://jsonplaceholder.typicode.com/todos/2")
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                responseMessage.StatusCode = HttpStatusCode.OK;
                responseMessage.Content = new StringContent(JsonSerializer.Serialize(new SampleJson
                {
                    Completed = true,
                    Id = 8675309,
                    Title = "Super Secret and Diabolical Plans",
                    UserId = 24
                }));

                return Task<HttpResponseMessage>.Factory.StartNew(() => responseMessage, cancellationToken);
            }

            if (httpRequestMessage?.RequestUri?.AbsoluteUri == "https://jsonplaceholder.typicode.com/todos/3")
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                responseMessage.StatusCode = HttpStatusCode.NotFound;
                responseMessage.Content = new StringContent("The resource didn't exist, yo.");
                return Task<HttpResponseMessage>.Factory.StartNew(() => responseMessage, cancellationToken);
            }

            if (httpRequestMessage?.RequestUri?.AbsoluteUri == "https://jsonplaceholder.typicode.com/todos/999999")
            {
                var httpRequestException = new HttpRequestException();
                throw new HttpRequestException();
            }

            throw new HttpRequestException("Resource Not Found", null, HttpStatusCode.NotFound);
        }
    }
}