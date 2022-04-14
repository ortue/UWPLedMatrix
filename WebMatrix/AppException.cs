using System;
using System.Globalization;

namespace WebMatrix
{
  public class AppException : Exception
  {
    public AppException() : base() { }

    public AppException(string message) : base(message) { }

    public AppException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {

    }
  }
}