using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Peitho.Infrastructure.Exceptions;
using Peitho.Utilities;

namespace Peitho.Services
{
    public class ApiRequestService : IApiRequestService
    {
        private readonly IAccessTokenProvider _tokenProvider;
        private readonly HttpClient _httpClient;

        public ApiRequestService(HttpClient httpClient, IAccessTokenProvider tokenProvider)
        {
            _httpClient = httpClient;
            _tokenProvider = tokenProvider;
        }

        /// <summary>
        /// Handles a GET request to an external API and attempts to deserialize the
        /// response to the type <typeparam name="TEntity"></typeparam>.
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public async Task<TEntity> HandleGetRequest<TEntity>(string requestUrl)
        {
            if (string.IsNullOrWhiteSpace(requestUrl))
            {
                throw new ArgumentNullException(nameof(requestUrl));
            }

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            {
                // Pass on the user's Bearer token to the API
                var tokenResult = await _tokenProvider.RequestAccessToken();

                if (tokenResult.TryGetToken(out var token))
                {
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", token.Value);
                }

                using (var response = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, new CancellationToken()))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var result = StreamUtilities.DeserializeJsonFromStream<TEntity>(stream);

                        return result;
                    }

                    var content = await StreamUtilities.StreamToStringAsync(stream);
                        
                    throw new ApiOperationException
                    {
                        StatusCode = (int) response.StatusCode,
                        Content = content
                    };
                }
            }
        }

        /// <summary>
        /// Handles a POST request to an external API and attempts to deserialize the
        /// response to the type <typeparam name="TEntity"></typeparam>.
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public async Task<TEntity> HandlePostRequest<TEntity>(string requestUrl, TEntity model)
        {
            if (string.IsNullOrWhiteSpace(requestUrl))
            {
                throw new ArgumentNullException(nameof(requestUrl));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl))
            using (var httpContent = CreateHttpContent(model))
            {
                // Pass on the user's Bearer token to the API
                var tokenResult = await _tokenProvider.RequestAccessToken();

                if (tokenResult.TryGetToken(out var token))
                {
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", token.Value);
                }

                // Attach the payload
                requestMessage.Content = httpContent;

                using (var response = await _httpClient.SendAsync(requestMessage,
                    HttpCompletionOption.ResponseHeadersRead, new CancellationToken()))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var result = StreamUtilities.DeserializeJsonFromStream<TEntity>(stream);

                        return result;
                    }

                    var content = await StreamUtilities.StreamToStringAsync(stream);
                        
                    throw new ApiOperationException
                    {
                        StatusCode = (int) response.StatusCode,
                        Content = content
                    };
                }
            }
        }
        
        // Original author: John Thiriet, https://johnthiriet.com
        private static HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                StreamUtilities.SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }
    }
}