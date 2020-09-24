using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Peitho.Shared
{
    public partial class AccessControl
    {
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] SignOutSessionStateManager SignOutManager { get; set; }

        private async Task BeginSignOut(MouseEventArgs args)
        {

            await SignOutManager.SetSignOutState();
            Navigation.NavigateTo("authentication/logout");
        }
    }
}