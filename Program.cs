using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peitho.Infrastructure;
using Peitho.Services;

namespace Peitho
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            
            builder.RootComponents.Add<App>("app");
            
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            builder.Services
                .AddOidcAuthentication(options =>
                {
                    builder.Configuration.Bind("Auth0", options.ProviderOptions);

                    options.ProviderOptions.ResponseType = "code";
                    options.UserOptions.RoleClaim = "https://schemas.jakarlse.com/roles";
                })
                .AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

            builder.Services.AddTransient<IApiRequestService, ApiRequestService>();
            
            await builder.Build().RunAsync();
        }
    }
}
