using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using appbase.Forms;
using bottomnavigation.Forms.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bottomnavigation.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomNavigationPage : RxContentPage
    {
        public BottomNavigationPage()
        {
            InitializeComponent();
            var viewModel = new ExampleViewModel
            {
                Sections = new ObservableCollection<NavigationItemModel>()
            };
            viewModel.Sections.Add(new NavigationItemModel
            {
                Title = "monkey 1",
                IconSource = "monkey"
            });

            viewModel.Sections.Add(new NavigationItemModel
            {
                Title = "monkey 2",
                IconSource = "monkey"
            });

            viewModel.Sections.Add(new NavigationItemModel
            {
                Title = "monkey ALONE blalalalalalalal",

            });

            viewModel.Sections.Add(new NavigationItemModel
            {
                IconSource = "monkey"
            });

            BindingContext = viewModel;
        }

        protected override void OnDisappearing()
        {
            BottomNavigation.Disposables.ForEach(disposable =>
            {
                disposable.Dispose();
            });
            base.OnDisappearing();
           
        }
    }
}
