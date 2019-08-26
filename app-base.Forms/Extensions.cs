using System;

using Xamarin.Forms;

namespace appbase.Forms
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string element) => String.IsNullOrEmpty(element);
        public static bool IsNotNullOrEmpty(this string element) => !String.IsNullOrEmpty(element);
    }
}

