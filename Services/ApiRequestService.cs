using System;
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
    }
}