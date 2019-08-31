using System;
using System.Collections.ObjectModel;
using bottomnavigation.Forms.Models;
using bottomnavigation.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
                pages.Add(new SectionModel
                {
                    NavigationItemModel = new NavigationItemModel
                    {
                        IconSource = "monkey",
                        Title = $"Monkey {i}"
                    }
                });
            }

            var viewModel = new BaseBottomNavigationPageModel
            {
                SelectionColor = Color.DarkBlue,
                NotSelectedColor = Color.DarkGray,
                ColorSeparetor = Color.LightGray,
                Pages = pages
            };

            MainPage = new BottomNavigationPage(viewModel);
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
