using System.Collections.ObjectModel;
using appbase.Forms;
using bottomnavigation.Forms.Exceptions;
using bottomnavigation.Forms.Models;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace bottomnavigation.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomNavigationPage : RxContentPage
    {
        private BaseBottomNavigationPageModel _viewModel;
        public BottomNavigationPage(BaseBottomNavigationPageModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            var bottomViews = new ObservableCollection<NavigationItemModel>();

            _viewModel.Pages.ForEach(page =>
            {
                bottomViews.Add(page.NavigationItemModel);
            });

            BottomNavigation.Sections = bottomViews;
            BindingContext = _viewModel;
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
