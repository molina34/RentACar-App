using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using RentACar.Entities;
using RentACar.Models;
using RentACar.Views;
using SQLite;
using Xamarin.Forms;

namespace RentACar.ViewModels
{
    public class CarInfoPageViewModel : ViewModelBase
    {
        public ICommand ConfirmCommand { get; set; }

        private Car _carSelected;
        public Car CarSelected
        {
            get { return _carSelected; }
            set { SetProperty(ref _carSelected, value); }
        }

        private DateTime _selectedDayInic;
        public DateTime SelectedDayInic
        {
            get { return _selectedDayInic; }
            set { SetProperty(ref _selectedDayInic, value); }
        }

        private DateTime _selectedDayFinal;
        public DateTime SelectedDayFinal
        {
            get { return _selectedDayFinal; }
            set { SetProperty(ref _selectedDayFinal, value); }
        }

        private DateTime _minDate;
        public DateTime MinDate
        {
            get { return _minDate; }
            set { SetProperty(ref _minDate, value); }
        }

        public CarInfoPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Car Info";

            ConfirmCommand = new DelegateCommand(ExecuteClickRent);

            MinDate = DateTime.Today;
        }

        private async void ExecuteClickRent()
        {
            DependencyService.Get<Control.ICalendarConnector>().AddAppointment(SelectedDayInic.AddHours(10), SelectedDayFinal.AddHours(12), "Rent: " + CarSelected.Name, "Rent a car store", "Rent A Car", false);

            SQLiteConnection conn = Database.Database.GetConnection();
            OrderEntity NewOrder = new OrderEntity
            {
                Model = CarSelected.Model,
                Brand = CarSelected.Brand,
                Color = CarSelected.Color,
                Price = CarSelected.Price,
                Year = CarSelected.Year,
                StartDate = SelectedDayInic.AddHours(10),
                EndDate = SelectedDayFinal.AddHours(12)
            };
            conn.Insert(NewOrder);

            await NavigationService.NavigateAsync("./" + nameof(MainPage));
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            CarSelected = parameters.GetValue<Car>("car");
        }
    }
}
