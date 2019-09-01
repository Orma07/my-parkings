using System;
using appbase.Forms.ViewModel;

namespace myparkings.Forms.ExamplePackage.ViewModels
{
    public class ViewModelExample : BaseViewModel
    {
        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }
    }
}
