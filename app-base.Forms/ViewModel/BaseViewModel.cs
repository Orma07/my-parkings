using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
namespace appbase.Forms.ViewModel
{
    /// <summary>
    /// Author: Abd Elgawwad, Amro
    /// To use it extend it and implement properties like this
    /// <code>
    /// private string title;
    ///
    /// public string Title
    /// {
    ///     get => title;
    ///     set => SetProperty(ref title, value);
    /// }
    ///
    /// </code>
    /// </summary>
    /// 
    public class BaseViewModel : BindableObject, INotifyPropertyChanged
    {



        #region INotifyPropertyChanged implementation
        protected void SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        public new void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
