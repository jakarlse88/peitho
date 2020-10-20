using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Peitho.Infrastructure;
using Peitho.Models;
using Peitho.Services;

namespace Peitho.Pages.Admin.TechniqueCategory
{
    public partial class Create
    {
        [Inject] private IApiRequestService ApiRequestService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        private const string requestUrl = "http://localhost:5002/api/technique/category";
        private TechniqueCategoryModel Model { get; set; }
        private ApiOperationState _apiOperationState = ApiOperationState.Initial;
        private EditContext _editContext;
        private bool _formIsInvalid;

        /// <summary>
        /// Component initialisation.
        /// </summary>
        protected override void OnInitialized()
        {
            // Submit button should start in 'disabled' state
            _formIsInvalid = true;
            
            // Init model
            Model = new TechniqueCategoryModel();
            
            // Init model and wire up event listener
            _editContext = new EditContext(Model);
            _editContext.OnFieldChanged += (sender, e) =>
            {
                _formIsInvalid = !_editContext.Validate();
                StateHasChanged();
            };
        }

        /// <summary>
        /// Form submission handler.
        /// </summary>
        /// <returns></returns>
        private async Task Submit()
        {
            _apiOperationState = ApiOperationState.OperationInProgress;
            StateHasChanged();

            var newModel = new TechniqueCategoryModel
            {
                Name = Model.Name,
                NameHangeul = Model.NameHangeul,
                NameHanja = Model.NameHanja
            };

            var result = await HandlePostRequest(newModel);
            
            NavigationManager.NavigateTo($"/Admin/Technique/Category/{result.Name}");
        }

        /// <summary>
        /// Handle API POST request.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private Task<TechniqueCategoryModel> HandlePostRequest(TechniqueCategoryModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            var result = ApiRequestService.HandlePostRequest<TechniqueCategoryModel>(requestUrl, model);

            return result;
        }
    }
}