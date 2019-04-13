using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using RentACar.Views;
using Xamarin.Forms;

namespace RentACar.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public ICommand ClickLoginCommand { get; set; }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Login Page";
            ClickLoginCommand = new DelegateCommand(async () => await ClickedLogin());
        }

        private async Task ClickedLogin()
        {
            if(Password == "123123")
            {
                await NavigationService.NavigateAsync("/"+nameof(NavigationPage)+"/"+ nameof(MainPage));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Login", "Wrong login or password, please try again.", "Ok");
            }
        }
    }
}
