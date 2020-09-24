using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Peitho.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Peitho.Pages.Admin
{
    public partial class Technique
    {
        [Inject] public IAccessTokenProvider TokenProvider { get; set; }
        [Inject] public HttpClient Http { get; set; }
        public TechniqueModel Model { get; set; }

        protected override async Task OnInitializedAsync()
        {
            const string techniqueName = "test";

            //using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5001/peitho/technique?name={techniqueName}"))
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5002/api/technique?name={techniqueName}"))
            {
                var tokenResult = await TokenProvider.RequestAccessToken();

                if (tokenResult.TryGetToken(out var token))
                {
                    requestMessage.Headers.Authorization =
                      new AuthenticationHeaderValue("Bearer", token.Value);

                    var response = await Http.SendAsync(requestMessage);
                    Model = await response.Content.ReadFromJsonAsync<TechniqueModel>();
                }
            }
        }
    }
}