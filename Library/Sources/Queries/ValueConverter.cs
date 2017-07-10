using System;
using CleenApi.Library.Exceptions;

namespace CleenApi.Library.Queries
{
  public static class ValueConverter
  {
    public static object Convert(string value, Type targetType)
    {
      try
      {
        if (targetType == typeof(string))
        {
          return value;
        }

        if (targetType == typeof(int))
        {
          return System.Convert.ToInt32(value);
        }

        if (targetType == typeof(bool))
        {
          return System.Convert.ToBoolean(value);
        }

        if (targetType.IsEnum)
        {
          return Enum.Parse(targetType, value);
        }
      }
      catch (Exception)
      {
        throw new EntityPropertyValueTypeNotSupportedException(value, targetType);
      }

      throw new EntityPropertyValueTypeNotSupportedException(value, targetType);
    }
  }
}