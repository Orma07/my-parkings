using System.Collections.ObjectModel;
using bottomnavigation.Forms.Models;
using bottomnavigation.Forms.Views;
using myparkings.Forms.ExamplePackage.Views;
using Xamarin.Forms;

namespace my_parkings
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var pages = new ObservableCollection<SectionModel>();

            for(int i = 1; i<5; i++)
            {
                var section = new SectionModel
                {
                    NavigationItemModel = new NavigationItemModel
                    {
                        IconSource = "monkey",
                        Title = $"Monkey {i}"

                    },
                    PagesType = i == 1 ? typeof(AppComponents) : typeof(ExampleContentView),
                };
                section.Args.Add("position", i);
                pages.Add(section);


            }

            var viewModel = new BaseBottomNavigationPageModel
            {
                SelectionColor = GetPrimaryColor(),
                NotSelectedColor = Color.DarkGray,
                ColorSeparetor = Color.LightGray,
                HeaderColor = Color.Transparent,
                IsNavigationHeaderVisible = true,
                Pages = pages
            };

            MainPage = new NavigationPage(new BottomNavigationPage(viewModel));

            //MainPage = new AppShell();
        }

        private Color GetPrimaryColor()
        {
            return (Color)Application.Current.Resources["PrimaryColor"];
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
