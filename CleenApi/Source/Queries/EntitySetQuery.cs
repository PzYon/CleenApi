using System;
using System.Collections.Generic;
using System.Linq;

namespace CleenApi.Queries
{
  public class EntitySetQuery
  {
    public int Take { get; }

    public int Skip { get; }

    public Dictionary<string, string> Conditions = new Dictionary<string, string>();

    public Dictionary<string, SortDirection> SortFields = new Dictionary<string, SortDirection>();

    public string[] Includes = new string[0];

    public EntitySetQuery(IEnumerable<KeyValuePair<string, string>> pairs)
    {
      foreach (KeyValuePair<string, string> pair in pairs)
      {
        switch (pair.Key)
        {
          case "$skip":
            Skip = Convert.ToInt32(pair.Value);
            break;

          case "$take":
            Take = Convert.ToInt32(pair.Value);
            break;

          case "$orderBy":
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

          case "$select":
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

    private string ToUpperCamelCase(string value)
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