using System;
using System.Collections.Generic;

namespace CleenApi.Entities
{
  public class EntitySetQuery
  {
    public int Take { get; }

    public int Skip { get; }

    public Dictionary<string, string> Conditions = new Dictionary<string, string>();

    public EntitySetQuery(KeyValuePair<string, string>[] pairs)
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
          default:
            Conditions[pair.Key] = pair.Value;
            break;
        }
      }
    }
  }
}