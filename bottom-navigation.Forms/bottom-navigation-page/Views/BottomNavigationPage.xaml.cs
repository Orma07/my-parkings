using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using appbase.Forms;
using bottomnavigation.Forms.Exceptions;
using bottomnavigation.Forms.Models;
using iconview.Forms;
using NavigationHeder.View;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace bottomnavigation.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomNavigationPage : RxContentPage
    {
        private BaseBottomNavigationPageModel _viewModel;
        public NavigationHeader NavigationHeder { get => NavigationHeaderVeiw; }
        private List<Type> _pagesStack;

        public BottomNavigationPage(BaseBottomNavigationPageModel viewModel)
        {
            InitializeComponent();
            _pagesStack = new List<Type>();
            _viewModel = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            var bottomViews = new ObservableCollection<NavigationItemModel>();

            _viewModel.Pages.ForEach(page =>
            {
                bottomViews.Add(page.NavigationItemModel);
            });

            BottomNavigation.Sections = bottomViews;
            BindingContext = _viewModel;
   
        }

        protected override bool OnBackButtonPressed()
        {
            if(_pagesStack.Count > 0)
            {
                var pageToPushType = _pagesStack.LastOrDefault();
                if (Activator.CreateInstance(pageToPushType) is View view)
                {
                    (PageContent.Content as IBottomNavigationPage).OnDestroy();

                    view.OnUIThread(() => PageContent.Content = view);

                    (view as IBottomNavigationPage).let(t =>
                    {
                        if (_pagesStack.Count == 1)
                        {
                            t.Args = _viewModel.Pages[BottomNavigation.Position].Args;
                            NavigationHeaderVeiw.let(NavHeader =>
                            {
                                NavHeader.LeftIconSource = null;
                                NavHeader.ResetLeftIcon();
                                NavHeader.ResetRightIcon();
                                NavHeader.RightIconSource = null; 

                            });
                        }


                        view.OnUIThread(() => t.OnCreate());
                      
                    });

                    _pagesStack.Remove(pageToPushType);
                }
                else
                {
                    throw new BottomNavigationPageException("PageType is not a View");
                }

                return false;
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }

    
        public static void PushPage(IBottomNavigationPage page)
        {
            if (Application.Current.MainPage is NavigationPage navigationPage
                && navigationPage.CurrentPage is BottomNavigationPage currentPage)
            {
                if (page is View)
                {
                    currentPage._pagesStack.Add(currentPage.PageContent.Content.GetType());
                    if (currentPage.PageContent.Content is IBottomNavigationPage)
                    {
                        (currentPage.PageContent.Content as IBottomNavigationPage).OnDestroy();
                    }

                    if (page.IsCurrentPlatform(Device.iOS))
                    {
                        currentPage.NavigationHeaderVeiw.let(t =>
                        {
                            t.LeftIconSource = DefaultIcons.LeftIcon;
                            t.LeftIconClick += (s, e) =>
                            {
                                currentPage.OnBackButtonPressed();
                            };
                        });
                    }

                    (page as View).let(view =>

                        view.OnUIThread(() =>
                        {
                            currentPage.PageContent.Content = page as View;
                            page.OnCreate();

                        })
                    ); 
                }
                else
                {
                    throw new BottomNavigationPageException("page is not a View");
                }
            }
            else
            {
                throw new BottomNavigationPageException("Current page is not a BottomNavigationPage");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindToLifeCycle(NavigationHeder.OnCreate());
            BindToLifeCycle(Observable.FromEventPattern<SectionClikedArgs>(
                handler => BottomNavigation.PositionChanged += handler,
                handler => BottomNavigation.PositionChanged -= handler)
                .Subscribe(args => OnPositionChanged(args.EventArgs)));
            BottomNavigation.Position = 0;

        }

        private void OnPositionChanged(SectionClikedArgs args)
        {
            var selectedType = _viewModel.Pages[args.PosionSelected].PagesType;
            if (!(Activator.CreateInstance(selectedType) is View view))
            {
                throw new BottomNavigationPageException("PageType is not a View");
            }
            if (!(view is IBottomNavigationPage bottomNavigationPage))
            {
                throw new BottomNavigationPageException("PageType is not a IBottomNavigationPage");
            }
            bottomNavigationPage.Args = _viewModel.Pages[args.PosionSelected].Args;
            if(PageContent.Content is IBottomNavigationPage)
            {
                (PageContent.Content as IBottomNavigationPage).OnDestroy();
            }
            PageContent.Content = view;
            bottomNavigationPage.OnCreate();

            NavigationHeaderVeiw.let(NavHeader =>
            {
                NavHeader.LeftIconSource = null;
                NavHeader.ResetLeftIcon();
                NavHeader.ResetRightIcon();
                NavHeader.RightIconSource = null;

            });


            NavigationHeaderVeiw.TitleText = _viewModel.Pages[args.PosionSelected].NavigationItemModel.Title;
        }

        protected override void OnDisappearing()
        {
            BottomNavigation.Disposables.ForEach(disposable =>
            {
                disposable.Dispose();
            });
            base.OnDisappearing();
           
        }
    }
}
