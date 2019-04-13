using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using RentACar.Views;

namespace RentACar.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand ClickAvailableCarsCommand { get; set; }
        public ICommand ClickOrdersCommand { get; set; }
        public ICommand ClickAccountCommand { get; set; }
        public ICommand ClickSettingsCommand { get; set; }
        public ICommand ClickContactCommand { get; set; }
        public ICommand ClickLogoutCommand { get; set; }





        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Main Page";

            ClickAvailableCarsCommand = new DelegateCommand(async () => await ClickedAvailableCarsCommand());
            ClickOrdersCommand = new DelegateCommand(async () => await ClickedOrdersCommand());
            ClickAccountCommand = new DelegateCommand(async () => await ClickedAccountCommand());
            ClickContactCommand = new DelegateCommand(async () => await ClickedContactCommand());
            ClickLogoutCommand = new DelegateCommand(async () => await ClickedLogoutCommand());
        }

        private async Task ClickedAvailableCarsCommand()
        {
            await NavigationService.NavigateAsync("./" + nameof(AvailableCarsPage));
        }

        private async Task ClickedOrdersCommand()
        {
            await NavigationService.NavigateAsync("./" + nameof(OrdersPage));
        }

        private async Task ClickedAccountCommand()
        {
            await NavigationService.NavigateAsync("./" + nameof(AccountPage));
        }

        private async Task ClickedContactCommand()
        {
            await NavigationService.NavigateAsync("./" + nameof(ContactPage));
        }

        private async Task ClickedLogoutCommand()
        {
            await NavigationService.NavigateAsync("/" + nameof(LoginPage));
        }
    }
}
