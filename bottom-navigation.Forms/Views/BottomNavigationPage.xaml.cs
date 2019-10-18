using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using appbase.Forms;
using bottomnavigation.Forms.Exceptions;
using bottomnavigation.Forms.Models;
using Xamarin.Forms;
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
            NavigationPage.SetHasNavigationBar(this, false);
            var bottomViews = new ObservableCollection<NavigationItemModel>();

            _viewModel.Pages.ForEach(page =>
            {
                bottomViews.Add(page.NavigationItemModel);
            });

            BottomNavigation.Sections = bottomViews;
            BindingContext = _viewModel;

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindToLifeCycle(Observable.FromEventPattern<SectionClikedArgs>(
                handler => BottomNavigation.PositionChanged += handler,
                handler => BottomNavigation.PositionChanged -= handler)
                .Subscribe(args => OnPositionChanged(args.EventArgs)));
            BottomNavigation.Position = 0;

        }

        private void OnPositionChanged(SectionClikedArgs args)
        {
            var selectedType = _viewModel.Pages[args.PosionSelected].PagesType;
            var view = Activator.CreateInstance(selectedType) as View;
            if(view == null)
            {
                throw new BottomNavigationPageException("PageType is not a View");
            }
            var bottomNavigationPage = view as IBottomNavigationPage;
            if (bottomNavigationPage == null)
            {
                throw new BottomNavigationPageException("PageType is not a IBottomNavigationPage");
            }
            bottomNavigationPage.Args = _viewModel.Pages[args.PosionSelected].Args;
            bottomNavigationPage.OnCreate();
            if(PageContent.Content is IBottomNavigationPage)
            {
                (PageContent.Content as IBottomNavigationPage).OnDestroy();
            }
            PageContent.Content = view;

            NavigationHeaderVeiw.TitleText = _viewModel.Pages[args.PosionSelected].NavigationItemModel.Title;
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
