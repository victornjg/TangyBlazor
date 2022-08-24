using Microsoft.AspNetCore.Components;
using System.Web;
using Tangy_Models;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Pages.Authentication
{
    public partial class Login
    {
        private SignInRequestDTO SignInRequest { get; set; } = new();
        private bool IsProcessing { get; set; } = false;
        private bool ShowSignInError { get; set; }
        public string Error { get; set; }
        public string ReturnUrl { get; set; }

        [Inject]
        private IAuthenticationService _authService { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        private async Task LoginUser()
        {
            ShowSignInError = false;
            IsProcessing = true;
            var response = await _authService.Login(SignInRequest);
            if (response.IsAuthSuccessful)
            {
                var absoluteUri = new Uri(_navigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnUrl = queryParam["returnUrl"];
                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    _navigationManager.NavigateTo("/");
                }
                else
                {
                    _navigationManager.NavigateTo("/" + ReturnUrl);
                }
            }
            else
            {
                Error = response.ErrorMessage;
                ShowSignInError = true;
            }
            IsProcessing = false;
        }
    }
}
