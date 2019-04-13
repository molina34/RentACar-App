using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using RentACar.Models;
using RentACar.Views;
using Xamarin.Forms;

namespace RentACar.ViewModels
{
    public class AvailableCarsPageViewModel : ViewModelBase
    {
        private List<Car> _carsList;
        public List<Car> CarsList
        {
            get { return _carsList; }
            set { SetProperty(ref _carsList, value); }
        }
        private Car _carSelected;
        public Car CarSelected
        {
            get { return _carSelected; }
            set { SetProperty(ref _carSelected, value); }
        }

        public ICommand ClickAvailableCarCommand { get; set; }

        public AvailableCarsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Available Cars";
            ClickAvailableCarCommand = new DelegateCommand(ClickedAvailableCar);

            CarsList = new List<Car>();
        }

        private async void ClickedAvailableCar()
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("car", CarSelected);

            await NavigationService.NavigateAsync("./" + nameof(CarInfoPage), parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            string url = "https://raw.githubusercontent.com/molina34/RentACar/master/API/cars.json";
            var client = new HttpClient();

            Task.Factory.StartNew(async () =>
            {
                var json = await client.GetStringAsync(string.Format(url));
                CarsList = JsonConvert.DeserializeObject<List<Car>>(json.ToString());
            });
        }
    }
}
