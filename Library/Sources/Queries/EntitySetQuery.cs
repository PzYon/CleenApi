using System;
using System.Collections.Generic;
using System.Linq;

namespace CleenApi.Library.Queries
{
  public class EntitySetQuery : IEntitySetQuery
  {
    public static class UrlKeys
    {
      public const string Skip = "$skip";
      public const string Take = "$take";
      public const string OrderBy = "$orderBy";
      public const string Select = "$select";
      public const string FullText = "q";
    }

    public int Take { get; }

    public int Skip { get; }

    public Dictionary<string, string> Conditions { get; } = new Dictionary<string, string>();

    public string FullText { get; }

    public Dictionary<string, SortDirection> SortFields { get; } = new Dictionary<string, SortDirection>();

    public string[] Includes { get; } = new string[0];

    public EntitySetQuery(IEnumerable<KeyValuePair<string, string>> pairs)
    {
      if (pairs == null)
      {
        return;
      }

      foreach (KeyValuePair<string, string> pair in pairs)
      {
        switch (pair.Key)
        {
          case UrlKeys.Skip:
            Skip = Convert.ToInt32(pair.Value);
            break;

          case UrlKeys.Take:
            Take = Convert.ToInt32(pair.Value);
            break;

          case UrlKeys.OrderBy:
            foreach (string sortField in pair.Value.Split(',').Select(v => v.Trim()))
            {
              if (sortField.StartsWith("-"))
              {
                SortFields[ToUpperCamelCase(sortField.Substring(1))] = SortDirection.Descending;
              }
              else
              {
                SortFields[ToUpperCamelCase(sortField)] = SortDirection.Ascending;
              }
            }
            break;

          case UrlKeys.FullText:
            FullText = pair.Value;
            break;

          case UrlKeys.Select:
            Includes = pair.Value
                           .Split(',')
                           .Select(v => v.Trim())
                           .Select(ToUpperCamelCase)
                           .ToArray();
            break;

          default:
            Conditions[ToUpperCamelCase(pair.Key)] = pair.Value;
            break;
        }
      }
    }

    private static string ToUpperCamelCase(string value)
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