using System;
using System.Collections.ObjectModel;
using appbase.Forms.ViewModel;
using Xamarin.Forms;

namespace bottomnavigation.Forms.Models
{
    public class BaseBottomNavigationPageModel : BaseViewModel
    {
        private Color _colorSeparetor;
        public Color ColorSeparetor
        {
            get => _colorSeparetor;
            set => SetProperty(ref _colorSeparetor, value);
        }

        private Color _selectionColor;
        public Color SelectionColor
        {
            get => _selectionColor;
            set => SetProperty(ref _selectionColor, value);
        }

        private Color _notSelectedColor;
        public Color NotSelectedColor
        {
            get => _notSelectedColor;
            set => SetProperty(ref _notSelectedColor, value);
        }

        private ObservableCollection<SectionModel> _pages;
        public ObservableCollection<SectionModel> Pages
        {
            get => _pages;
            set => SetProperty(ref _pages, value);
        }

    }
}
