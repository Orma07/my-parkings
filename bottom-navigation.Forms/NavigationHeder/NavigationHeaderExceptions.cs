using System;
namespace NavigationHeder
{
    public class NavigationHeaderExceptions: Exception
    {
        private string _message;
        public override string Message { get => _message; }
        public NavigationHeaderExceptions(string message)
        {
            _message = message;
        }
       
    }
}
