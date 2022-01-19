using System.Globalization;
using System;
/*
    Custom  define Exception Class
*/

namespace webApi.Helper
{
    public class AppException: Exception
    {
        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args) 
        : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}

