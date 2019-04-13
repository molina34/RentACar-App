using System;
using Prism;
using Prism.Ioc;
using Prism.DryIoc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RentACar.ViewModels;
using RentACar.Views;

namespace RentACar
{
    public partial class App
    {
        public App() : this(null) { }
        public App(IPlatformInitializer platformInitializer) : base(platformInitializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<AvailableCarsPage>();
            containerRegistry.RegisterForNavigation<CarInfoPage>();
            containerRegistry.RegisterForNavigation<OrdersPage>();
        }

    }
}
