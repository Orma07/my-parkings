using System;
using my_parkings;
using Xamarin.Forms;

namespace myparkings.Forms.Utils
{
    public static class MyParkingExtensions
    {
        public static Color GetPrimaryColor(this View view)
        {
            return (Color)Application.Current.Resources["PrimaryColor"];
        }

        public static Color GetPrimaryLightColor(this View view)
        {
            return (Color)Application.Current.Resources["PrimaryLightColor"];
        }

        public static Color GetPrimaryDarkColor(this View view)
        {
            return (Color)Application.Current.Resources["PrimaryDarkColor"];
        }
    }
}
