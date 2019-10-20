using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace appbase.Forms
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string element) => String.IsNullOrEmpty(element);
        public static bool IsNotNullOrEmpty(this string element) => !String.IsNullOrEmpty(element);
        /// <summary>
        /// Add am Observable to click event to any view
        /// </summary>
        /// <param name="view"></param>
        /// <param name="OnClick">Action to perform has 2 arguments first one is sender and second one is EventArgs</param>
        /// <param name="throttleMilliSeconds">Time in ms to throttle events on stream</param>
        /// <returns></returns>
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

        /// <summary>
        /// Add am Observable to click event to any view
        /// </summary>
        /// <param name="view"></param>
        /// <param name="OnClick">Action to perform</param>
        /// <param name="throttleMilliSeconds">Time in ms to throttle events on stream</param>
        /// <returns></returns>
        public static IDisposable OnClick(this View view, Action OnClick, int throttleMilliSeconds = 0)
        {
            var tapGesture = new TapGestureRecognizer();
            view.GestureRecognizers.Add(tapGesture);

            var observable = Observable.FromEventPattern(
                handler => tapGesture.Tapped += handler,
                handler => tapGesture.Tapped -= handler);
            if (throttleMilliSeconds > 0)
            {
                observable = observable.Throttle(TimeSpan.FromMilliseconds(throttleMilliSeconds));
            }

            return observable.Subscribe(eventHandler =>
            {
                OnClick();
            });

        }

        public static void OnUIThread(this View view, Action action) => Device.BeginInvokeOnMainThread(action);
   
        public static void let<T>(this T t, Action<T> action) => action(t);

        public static void OnNewTask(this object current, Func<Task> actionAsync)
        {
            IDisposable disposable = null;
            disposable = Observable.FromAsync(actionAsync)
                .SubscribeOn(NewThreadScheduler.Default)
                .ObserveOn(NewThreadScheduler.Default)
                .Subscribe(t => disposable?.Dispose());
        }

        public static bool IsNotNull(this object current)
        {
            return current != null;
        }
    }

}

