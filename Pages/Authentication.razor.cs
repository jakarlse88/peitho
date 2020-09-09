using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

namespace Peitho.Pages
{
    public partial class Authentication
    {
        [Parameter] public string Action { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IConfiguration Configuration { get; set; }
    }
}