using System;
namespace bottomnavigation.Forms.Exceptions
{
    public class BottomNavigationPageException : Exception
    {
        private string _message;
        public override string Message{ get => _message; }

        public BottomNavigationPageException(string message)
        {
            _message = message;
        }
    }
}
