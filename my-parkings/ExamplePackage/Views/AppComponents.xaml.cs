using System;
using System.Collections.Generic;
using appbase.Forms;
using bottomnavigation.Forms;
using bottomnavigation.Forms.Views;
using Xamarin.Forms;

namespace myparkings.Forms.ExamplePackage.Views
{
    public partial class AppComponents : ContentView, IBottomNavigationPage
    {

        public AppComponents()
        {
            InitializeComponent();
            TestInverseButton.Clicked += (s, e) =>
            {
                BottomNavigationPage.PushPage(new ExampleContentView());
            };
            
        }

        private Dictionary<string, object> _args;
        public Dictionary<string, object> Args { get => _args; set => _args = value; }

        public void OnCreate()
        {
            

        }

        public void OnDestroy()
        {
            
        }
    }
}
