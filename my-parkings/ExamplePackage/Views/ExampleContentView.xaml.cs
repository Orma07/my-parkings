using System;
using System.Collections.Generic;
using appbase.Forms;
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
            var position = -1;
            try
            {
                position = (int)_args["position"];
            }
            catch
            {

            }

            var viewModel = new ViewModelExample();

            viewModel.Text.let(t =>
            {
                if (position > -1)
                {
                    t = position.ToString();
                }
                else
                {
                    t = "new page inside section";
                }
            });

            Device.BeginInvokeOnMainThread(() => {
                BindingContext = viewModel;
            });
        }

        public void OnDestroy()
        {
            
        }
    }
}
