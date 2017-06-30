using System;
using System.Collections.Generic;
using System.Linq;

namespace CleenApi.Entities.Queries
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
                SortFields[sortField.Substring(1)] = SortDirection.Descending;
              }
              else
              {
                SortFields[sortField] = SortDirection.Ascending;
              }
            }
            break;

          case "$select":
            Includes = pair.Value.Split(',').Select(v => v.Trim()).ToArray();
            break;

          default:
            Conditions[pair.Key] = pair.Value;
            break;
        }
      }
    }
  }
}