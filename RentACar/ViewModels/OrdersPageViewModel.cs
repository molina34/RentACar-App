using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using RentACar.Database;
using RentACar.Entities;
using RentACar.Models;
using RentACar.Views;
using SQLite;
using Xamarin.Forms;

namespace RentACar.ViewModels
{
    public class OrdersPageViewModel : ViewModelBase
    {
        private List<OrderEntity> _ordersList;
        public List<OrderEntity> OrdersList
        {
            get { return _ordersList; }
            set { SetProperty(ref _ordersList, value); }
        }

        public OrdersPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Orders";

            OrdersList = new List<OrderEntity>();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            SQLiteConnection conn = Database.Database.GetConnection();
            OrdersList = conn.Table<OrderEntity>().OrderBy(a=> a.StartDate).ToList();
        }
    }
}
