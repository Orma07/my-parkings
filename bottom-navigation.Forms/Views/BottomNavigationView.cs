using System;
using System.Collections.ObjectModel;
using bottomnavigation.Forms.Models;
using bottomnavigation.Forms.Utils;
using iconview.Forms;
using Xamarin.Forms;

namespace bottomnavigation.Forms.Views
{
    public class BottomNavigationView : Grid
    {
        #region Sections
        public static readonly BindableProperty SectionsProperty =
        BindableProperty.Create("Sections",
            typeof(ObservableCollection<NavigationItemModel>),
            typeof(BottomNavigationView),
            new ObservableCollection<NavigationItemModel>(),
            propertyChanged: OnSectionsChanged);



        private static void OnSectionsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (BottomNavigationView)bindable;
  
            for(int i = 0; i < current.Sections.Count; i++)
            {
                current.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)});
            }
            foreach(var model in current.Sections)
            {
                var position = current.Sections.IndexOf(model);
                switch (model.GetViewType())
                {
                    case SectionViewType.IcontAndText:
                        current.addIconAndText(model, position);
                        break;
                    case SectionViewType.OnlyIcon:
                        current.addIcon(model.IconSource, position);
                        break;
                    case SectionViewType.OnlyText:
                        current.addTitle(model.Title, position);
                        break;
                        
                }
            }

        }

        private void addTitle(string title, int position)
        {
            Children.Add(new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                LineBreakMode = LineBreakMode.TailTruncation,
                Text = title
            }, position, 0);
        }

        private void addIcon(string iconSource, int position)
        {
            Children.Add(new IconView
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Source = iconSource
            }, position, 0);
        }

        private void addIconAndText(NavigationItemModel model, int position)
        {
            var titleLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = model.Title,
                LineBreakMode = LineBreakMode.TailTruncation
            };

            var iconView = new IconView
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Source = model.IconSource
            };

            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.67, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.33, GridUnitType.Star) });
            grid.Children.Add(iconView, 0, 0);
            grid.Children.Add(titleLabel, 0, 1);

            Children.Add(grid, position, 0);
        }

        public ObservableCollection<NavigationItemModel> Sections
        {
            get { return (ObservableCollection<NavigationItemModel>)GetValue(SectionsProperty); }
            set { SetValue(SectionsProperty, value); }
        }
        #endregion
      
    }
}

