using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using appbase.Forms;
using bottomnavigation.Forms.Models;
using bottomnavigation.Forms.Utils;
using iconview.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace bottomnavigation.Forms.Views
{
    /// <summary>
    /// Author: Amro, Abd Elgawwad
    /// </summary>
    public class BottomNavigationView : Grid
    {

        private List<View> _sectionsView = new List<View>();

        public EventHandler<SectionClikedArgs> PositionChanged;
        public int _position;
        public int Position
        {
            get => _position;
            set
            {
                _position = value;
                PositionChanged?.Invoke(this, new SectionClikedArgs{
                    PosionSelected = value
                });
            }
        }
        public List<IDisposable> Disposables = new List<IDisposable>();

        #region SelectionColor
        public static readonly BindableProperty SelectionColorProperty =
        BindableProperty.Create("SelectionColor",
            typeof(Color),
            typeof(BottomNavigationView),
            Color.Blue,
            propertyChanged: OnSelectionColorChanged);

        private static void OnSelectionColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }

        public Color SelectionColor
        {
            get { return (Color)GetValue(SelectionColorProperty); }
            set { SetValue(SelectionColorProperty, value); }
        }

        #endregion

        #region NotSelectedColor
        public static readonly BindableProperty NotSelectionColorProperty =
        BindableProperty.Create("NotSelectionColor",
            typeof(Color),
            typeof(BottomNavigationView),
            Color.Black,
            propertyChanged: OnNotSelectionColorChanged);

        private static void OnNotSelectionColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (BottomNavigationView)bindable;
            foreach(var element in current._sectionsView)
            {
                if(element is IconView)
                {
                    var icon = element as IconView;
                    icon.LineColor = current.NotSelectionColor;
                    icon.InvalidateSurface();
                }
                else if(element is Label)
                {
                    var title = element as Label;
                    title.TextColor = current.NotSelectionColor;
                }
                
            }
        }

        public Color NotSelectionColor
        {
            get { return (Color)GetValue(NotSelectionColorProperty); }
            set { SetValue(SelectionColorProperty, value); }
        }
        #endregion

        #region SeparetorColorProperty
        public static readonly BindableProperty SeparetorColorProperty =
        BindableProperty.Create("SeparetorColor",
            typeof(Color),
            typeof(BottomNavigationView),
            Color.LightGray,
            propertyChanged: OnSeparetorColorChanged);

        private static void OnSeparetorColorChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        public Color SeparetorColor
        {
            get { return (Color)GetValue(SeparetorColorProperty); }
            set { SetValue(SeparetorColorProperty, value); }
        }
        #endregion

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

            current.ColumnDefinitions.Clear();
            current.Children.Clear();
  
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
                        current.AddIconAndText(model, position);
                        break;
                    case SectionViewType.OnlyIcon:
                        current.AddIcon(model.IconSource, position);
                        break;
                    case SectionViewType.OnlyText:
                        current.AddTitle(model.Title, position);
                        break;
                        
                }
            }

        }

        public ObservableCollection<NavigationItemModel> Sections
        {
            get { return (ObservableCollection<NavigationItemModel>)GetValue(SectionsProperty); }
            set { SetValue(SectionsProperty, value); }
        }
        #endregion


        private Label CreateTitleLabel(string title)
        {
            var label = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                LineBreakMode = LineBreakMode.TailTruncation,
                TextColor = NotSelectionColor,
                Text = title
            };
            
            return label;
        }

        private IconView CreateIconView(string source)
        {
            var iconView = new IconView
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                LineColor = NotSelectionColor,
                Source = source
            };
           
            return iconView;
        }

        private void SelectIcon(object sender, object args)
        {
            DeselectIcons();
            IconView iconView = null;
            Label label = null;
            if(sender is Grid)
            {
                var grid = sender as Grid;
                iconView = grid.Children[0] as IconView;
                label = grid.Children[1] as Label;
            }
            else if(sender is IconView)
            {
                iconView = sender as IconView;
               
            }
            else if(sender is Label)
            {
                label = sender as Label;
                
            }

            if(iconView != null)
            {
                iconView.LineColor = SelectionColor;
                iconView.InvalidateSurface();

            }
            if(label != null)
            {
                label.TextColor = SelectionColor;
            }

            Position = _sectionsView.IndexOf(sender);
        }

        private void DeselectIcons()
        {
            _sectionsView.ForEach(element =>
            {
                IconView iconView = null;
                Label label = null;
                if (element is Grid)
                {
                    var grid = element as Grid;
                    iconView = grid.Children[0] as IconView;
                    label = grid.Children[1] as Label;
                }
                else if (element is IconView)
                {
                    iconView = element as IconView;

                }
                else if (element is Label)
                {
                    label = element as Label;

                }

                if (iconView != null)
                {
                    iconView.LineColor = NotSelectionColor;
                    iconView.InvalidateSurface();

                }
                if (label != null)
                {
                    label.TextColor = NotSelectionColor;
                }
            });
        }

        private void AddTitle(string title, int position)
        {
            var label = CreateTitleLabel(title);
            Disposables.Add(label.OnClick(SelectIcon));
            _sectionsView.Add(label);
            Children.Add(label, position, 0);
        }

        private void AddIcon(string iconSource, int position)
        {
            var icon = CreateIconView(iconSource);
            _sectionsView.Add(icon);
            Disposables.Add(icon.OnClick(SelectIcon));
            Children.Add(icon, position, 0);
        }

        private void AddIconAndText(NavigationItemModel model, int position)
        {
            var titleLabel = CreateTitleLabel(model.Title);
            titleLabel.VerticalOptions = LayoutOptions.Start;
            //titleLabel.BackgroundColor = Color.Green;

            var iconView = CreateIconView(model.IconSource);
            iconView.VerticalOptions = LayoutOptions.End;
            //iconView.BackgroundColor = Color.Yellow;

 
            Grid grid = new Grid();
            grid.RowSpacing = 0;
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.6, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.4, GridUnitType.Star) });
            grid.Children.Add(iconView, 0, 0);
            grid.Children.Add(titleLabel, 0, 1);

            Disposables.Add(grid.OnClick(SelectIcon));
            _sectionsView.Add(grid);
            Children.Add(grid, position, 0);
        }
    }

    public class SectionClikedArgs : EventArgs
    {
        public int PosionSelected { get; set; }
    }
}

