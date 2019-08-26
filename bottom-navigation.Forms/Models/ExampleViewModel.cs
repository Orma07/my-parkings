using System;
using System.Collections.ObjectModel;
using appbase.Forms.ViewModel;
using Xamarin.Forms;

namespace bottomnavigation.Forms.Models
{
    public class ExampleViewModel : BaseViewModel
    {
        private ObservableCollection<NavigationItemModel> _sections;
        public ObservableCollection<NavigationItemModel> Sections
        {
            get => _sections;
            set => SetProperty(ref _sections, value);
        }
    }
}

