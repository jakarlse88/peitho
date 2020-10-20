using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Peitho.Models;
using Peitho.Services;

namespace Peitho.Pages.Admin.TechniqueCategory
{
    public partial class Index
    {
        [Inject] private IApiRequestService ApiRequestService { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }
        private IEnumerable<TechniqueCategoryModel> Models { get; set; }
        private const string TableId = "categoryTable";
        
        // TODO: single TechniqueCategory page
        // TODO: consider refactoring page .containers to the layout
        
        /// <summary>
        /// Component initialisation logic.
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            const string requestUrl = "http://localhost:5002/api/technique/all";
            
            Models = 
                await ApiRequestService.HandleGetRequest<IEnumerable<TechniqueCategoryModel>>(requestUrl);
            
            StateHasChanged();
            
            await JsRuntime.InvokeAsync<object>("Peitho.CreateDataTable", TableId);

            await base.OnInitializedAsync();
        }
    }
}