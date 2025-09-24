
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.App.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly GlobalViewModel _global;

        [ObservableProperty]
        private string name = "";

        [ObservableProperty]
        private string email = "";

        [ObservableProperty]
        private string password = "";

        [ObservableProperty]
        private string registerMessage;

        public RegisterViewModel(IAuthService authService, GlobalViewModel global)
        {
            _authService = authService;
            _global = global;
        }

        [RelayCommand]
        private void Register()
        {
            Client? registerClient = _authService.Register(Name, Email, Password);
            if (registerClient != null)
            {
                RegisterMessage = "Registratie succesvol!";
                Application.Current.MainPage = new LoginView(new LoginViewModel(_authService, _global));
            }
            else
            {
                RegisterMessage = "Registratie mislukt.";
            }
        }
    }
}
