using System;
using System.Collections.Generic;
using bottomnavigation.Forms;
using myparkings.Forms.ExamplePackage.ViewModels;
using Xamarin.Forms;

namespace myparkings.Forms.ExamplePackage.Views
{
    public partial class ExampleContentView : ContentView, IBottomNavigationPage
    {
        private Dictionary<string, object> _args = new Dictionary<string, object>();
        public Dictionary<string, object> Args { get => _args; set => _args = value; }

        public ExampleContentView()
        {
            InitializeComponent();
        }

        public void OnCreate()
        {
            var position = (int)_args["position"];
            var viewModel = new ViewModelExample
            {
                Text = position.ToString(),
            };
            Device.BeginInvokeOnMainThread(() => {
                BindingContext = viewModel;
            });
        }

        public void OnDestroy()
        {
            
        }
    }
}
