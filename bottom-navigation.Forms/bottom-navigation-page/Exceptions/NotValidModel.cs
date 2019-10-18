using System;

using Xamarin.Forms;

namespace bottomnavigation.Forms.Exceptions
{
    public class NotValidModel : Exception
    {
        private string _message;
        public override string Message => _message;

        public NotValidModel(Type type)
        {
            _message = $"{type} is not a valid model for BottomNavigation";
        }

        public NotValidModel()
        {
            _message = $"This is not a valid model for BottomNavigation, Title or IconSource mus be not null";
        }

        public NotValidModel(string message)
        {
            _message = message;
        }

    }
}

