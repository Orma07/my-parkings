using System;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace appbase.Forms
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string element) => String.IsNullOrEmpty(element);
        public static bool IsNotNullOrEmpty(this string element) => !String.IsNullOrEmpty(element);
        public static IDisposable OnClick(this View view, Action<object, object> OnClick, int throttleMilliSeconds = 0)
        {
            var tapGesture = new TapGestureRecognizer();
            view.GestureRecognizers.Add(tapGesture);

            var observable = Observable.FromEventPattern(
                handler => tapGesture.Tapped += handler,
                handler => tapGesture.Tapped -= handler);
            if(throttleMilliSeconds > 0)
            {
                observable = observable.Throttle(TimeSpan.FromMilliseconds(throttleMilliSeconds));
            }

            return observable.Subscribe(eventHandler =>
            {
                    OnClick(eventHandler.Sender, eventHandler.EventArgs);
            });

        }
    }

}

