using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace appbase.Forms
{
    public class RxContentPage : ContentPage
    {
        private List<IDisposable> _disposable = new List<IDisposable>();

        /// <summary>
        /// call it onAppearing
        /// </summary>
        /// <param name="disposable"></param>
        public void BindToLifeCycle(IDisposable disposable)
        {
            _disposable.Add(disposable);
        }

        protected override void OnDisappearing()
        {
  
            _disposable.ForEach(disposable =>
            {
                disposable.Dispose();
            });
            base.OnDisappearing();
        }

        public RxContentPage()
        {
           
        }
    }
}

