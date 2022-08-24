using Microsoft.AspNetCore.Components;
using Tangy_Models;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Pages.Authentication
{
    public partial class Register
    {
        private SignUpRequestDTO SignUpRequest { get; set; } = new ();
        private bool IsProcessing { get; set; } = false;
        private bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        [Inject]
        private IAuthenticationService _authService { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }


        private async Task RegisterUser()
        {
            ShowRegistrationErrors = false;
            IsProcessing = true;
            var response = await _authService.RegisterUser(SignUpRequest);
            if (response.IsRegisterationSuccessful)
            {
                _navigationManager.NavigateTo("/login");
            }
            else
            {
                Errors = response.Errors;
                ShowRegistrationErrors = true;
            }
            IsProcessing = false;
        }
    }
}
