using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Peitho.Models;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Peitho.Services;

namespace Peitho.Pages.Admin
{
    public partial class Technique
    {
        [Inject] private IApiRequestService ApiRequestService { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }
        private IEnumerable<TechniqueModel> Techniques { get; set; }
        private const string TableId = "techniquesTable";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
            }
        }

        protected override async Task OnInitializedAsync()
        {
            const string requestUrl = "http://localhost:5002/api/technique/all";
            
            Techniques = 
                await ApiRequestService.HandleGetRequest<IEnumerable<TechniqueModel>>(requestUrl);
            
            StateHasChanged();
            
            await JsRuntime.InvokeAsync<object>("Peitho.CreateDataTable", TableId);

            await base.OnInitializedAsync();
        }
    }
}