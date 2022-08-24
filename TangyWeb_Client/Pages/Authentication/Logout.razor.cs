using Microsoft.AspNetCore.Components;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        private IAuthenticationService _authService { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await _authService.Logout();
            _navigationManager.NavigateTo("/");
        }
    }
}
