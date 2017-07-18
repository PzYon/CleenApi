namespace CleenApi.Library.Utilities
{
  public static class StringUtility
  {
    public static string ToCamelCase(string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return value;
      }

      char firstChar = value[0];
      if (char.IsLower(firstChar))
      {
        return value;
      }

      return char.ToLower(firstChar) + value.Substring(1);
    }

    public static string ToUpperCamelCase(string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return value;
      }

      char firstChar = value[0];
      if (char.IsUpper(firstChar))
      {
        return value;
      }

      return char.ToUpper(firstChar) + value.Substring(1);
    }
  }
}
