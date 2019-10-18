using System;
using appbase.Forms;
using bottomnavigation.Forms.Exceptions;
using bottomnavigation.Forms.Models;
using Xamarin.Forms;

namespace bottomnavigation.Forms.Utils
{
    public static class BottomNavigationExtensions
    {
        public static SectionViewType GetViewType(this NavigationItemModel element)
        {
            if (element.Title.IsNotNullOrEmpty() && element.IconSource.IsNotNullOrEmpty())
            {
                return SectionViewType.IcontAndText;
            }
            else if (element.Title.IsNotNullOrEmpty())
            {
                return SectionViewType.OnlyText;
            }
            if (element.IconSource.IsNotNullOrEmpty())
            {
                return SectionViewType.OnlyIcon;
            }
            else
            {
                throw new NotValidModel();
            }
        }
    }
}

