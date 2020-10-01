using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Peitho.Models;
using System.Threading.Tasks;
using Peitho.Services;

namespace Peitho.Pages.Admin
{
    public partial class Technique
    {
        [Inject] private IApiRequestService ApiRequestService { get; set; }
        private IEnumerable<TechniqueModel> Techniques { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Techniques = new List<TechniqueModel>();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            const string requestUrl = "http://localhost:5002/api/technique/all";
            
            Techniques = 
                await ApiRequestService.HandleGetRequest<IEnumerable<TechniqueModel>>(requestUrl);
        }
    }
}