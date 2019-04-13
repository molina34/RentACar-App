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
    public class ContactPageViewModel : ViewModelBase
    {
        public ContactPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Contact";
        }
    }
}
