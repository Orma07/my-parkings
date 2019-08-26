using System;

using Xamarin.Forms;

namespace bottomnavigation.Forms.Resources
{
    public class Class1 : ContentPage
    {
        public Class1()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

