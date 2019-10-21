using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace appbase.Forms.BaseViews
{
    public class RxContentView : ContentView
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
    }
}

