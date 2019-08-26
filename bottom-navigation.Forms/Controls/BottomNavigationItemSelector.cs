using System;
using appbase.Forms;
using bottomnavigation.Forms.Exceptions;
using bottomnavigation.Forms.Models;
using Xamarin.Forms;

namespace bottomnavigation.Forms.Controls
{
    /// <summary>
    /// Author: Abd Elgawwad, Amro
    /// </summary>
    public class BottomNavigationItemSelector : DataTemplateSelector
    {
        public DataTemplate IconTextTemplate { get; set; }
        public DataTemplate IconTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var currentItem = item as NavigationItemModel;
            if(currentItem != null)
            {
                if (currentItem.Title.IsNotNullOrEmpty() && currentItem.IconSource.IsNotNullOrEmpty())
                    return IconTextTemplate;
                else if (currentItem.Title.IsNotNullOrEmpty() && currentItem.IconSource.IsNullOrEmpty())
                    return TextTemplate;
                else if (currentItem.Title.IsNullOrEmpty() && currentItem.IconSource.IsNotNullOrEmpty())
                    return IconTemplate;
                else
                    throw new NotValidModel();
            }
            else
            {
                throw new NotValidModel(item.GetType());
            }
        }
    }
}

