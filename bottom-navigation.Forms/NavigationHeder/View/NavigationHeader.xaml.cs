using System;
using System.Collections.Generic;
using appbase.Forms;
using Xamarin.Forms;

namespace NavigationHeder.View
{
    public partial class NavigationHeader : Grid, IDisposable
    {
        #region LeftIconSource
        public event EventHandler LeftIconClick;
        public static readonly BindableProperty LeftIconSourceProperty =
        BindableProperty.Create("LeftIconSource",
            typeof(string),
            typeof(NavigationHeader),
            "",
            propertyChanged: OnLeftIconSourceChange);

        private static void OnLeftIconSourceChange(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (NavigationHeader)bindable;
            try
            {
                current._leftIcon.Source = newValue as string;
            }
            catch
            {
                throw new NavigationHeaderExceptions("Invalid LeftIconSource");
            }
        }

        public string LeftIconSource
        {
            get { return (string)GetValue(LeftIconSourceProperty); }
            set { SetValue(LeftIconSourceProperty, value); }
        }
        #endregion

        #region RightIconSource
        public event EventHandler RightIconClick;
        public static readonly BindableProperty RightIconSourceProperty =
        BindableProperty.Create("RightIconSource",
            typeof(string),
            typeof(NavigationHeader),
            "",
            propertyChanged: OnRightIconSourceChange);

        private static void OnRightIconSourceChange(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (NavigationHeader)bindable;
            try
            {
                current._rightIcon.Source = newValue as string;
            }
            catch
            {
                throw new NavigationHeaderExceptions("Invalid RightIconSource");
            }
        }

      
        public string RightIconSource
        {
            get { return (string)GetValue(RightIconSourceProperty); }
            set { SetValue(LeftIconSourceProperty, value); }
        }
        #endregion

        #region TitleText
        public static readonly BindableProperty TitleTextProperty =
        BindableProperty.Create("TitleText",
            typeof(string),
            typeof(NavigationHeader),
            "",
            propertyChanged: OnTitleTextChange);

        private static void OnTitleTextChange(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (NavigationHeader)bindable;
            try
            {
                current._titleLabel.Text = newValue as string;
            }
            catch
            {
                throw new NavigationHeaderExceptions("Invalid TitleText");
            }
        }


        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }
        #endregion

        #region TitleTextColor
        public static readonly BindableProperty TitleTextColorProperty =
        BindableProperty.Create("TitleTextColor",
            typeof(Color),
            typeof(NavigationHeader),
            Color.Black,
            propertyChanged: OnTitleTextColorChange);

        private static void OnTitleTextColorChange(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (NavigationHeader)bindable;
            try
            {
                current._titleLabel.TextColor = current.TitleTextColor;
                current._rightIcon.LineColor = current.TitleTextColor;
                current._leftIcon.LineColor = current.TitleTextColor;

            }
            catch
            {
                throw new NavigationHeaderExceptions("Invalid TitleTextColor");
            }
        }


        public Color TitleTextColor
        {
            get { return (Color)GetValue(TitleTextColorProperty); }
            set { SetValue(TitleTextColorProperty, value); }
        }
        #endregion

        #region TitleTextSize
        public static readonly BindableProperty TitleTextSizeProperty =
        BindableProperty.Create("TitleTextSize",
            typeof(double),
            typeof(NavigationHeader),
            22d,
            propertyChanged: OnTitleTextSizeChange);

        private static void OnTitleTextSizeChange(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (NavigationHeader)bindable;
            try
            {
                current._titleLabel.FontSize = (double)newValue;
            }
            catch
            {
                throw new NavigationHeaderExceptions("Invalid TitleTextSize");
            }
        }


        public double TitleTextSize
        {
            get { return (double)GetValue(TitleTextSizeProperty); }
            set { SetValue(TitleTextSizeProperty, value); }
        }
        #endregion

        #region TitleTextFont
        public static readonly BindableProperty TitleTextFontProperty =
        BindableProperty.Create("TitleTextFont",
            typeof(string),
            typeof(NavigationHeader),
            "",
            propertyChanged: OnTitleTextFontChange);

        private static void OnTitleTextFontChange(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (NavigationHeader)bindable;
            try
            {
                var fontFamily = newValue as string;
                if (fontFamily.IsNotNullOrEmpty())
                {
                    current._titleLabel.FontFamily = fontFamily;
                }
               
            }
            catch
            {
                throw new NavigationHeaderExceptions("Invalid TitleTextFont");
            }
        }


        public string TitleTextFont
        {
            get { return (string)GetValue(TitleTextFontProperty); }
            set { SetValue(TitleTextFontProperty, value); }
        }
        #endregion

        private List<IDisposable> Disposables;

        public NavigationHeader()
        {
            InitializeComponent();
            Disposables = new List<IDisposable>();
        }

        /// <summary>
        /// must be called by the consumer, during OnAppearing, to Bind the component to the lifeCycle of current Page
        /// </summary>
        /// <returns></returns>
        public IDisposable OnCreate()
        {
            Disposables.Add(_leftIcon.OnClick(() => LeftIconClick?.Invoke(this, EventArgs.Empty), 1));
            Disposables.Add(_rightIcon.OnClick(() => RightIconClick?.Invoke(this, EventArgs.Empty), 1));
            return this;
        }

        public void Dispose()
        {
            foreach(var disposable in Disposables)
            {
                disposable?.Dispose();
            }
        }


    }
}
