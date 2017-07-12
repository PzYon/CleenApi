using System;

namespace CleenApi.Library.Exceptions
{
  public class InvalidRequestException : Exception, IInvalidRequestException
  {
    public InvalidRequestException(string message) : base(message)
    {
    }
  }
}