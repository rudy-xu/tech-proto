/*
    Costum define exception
*/

using System;
using System.Globalization;

namespace WebApi.Helpers
{
    public class AppException: Exception
    {
        public AppException(string message): base(message) {}

        public AppException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args)){}
    }
}