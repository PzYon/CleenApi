using System;
using System.Collections.Generic;
using System.Linq;
using CleenApi.Library.Utilities;

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

    public static class Chars
    {
      public static readonly char WildCard = '*';
      public static readonly char Separator = ',';
      public static readonly char Descending = '-';
      public static readonly char NotEqual = '!';
    }

    public int Take { get; }

    public int Skip { get; }

    public Dictionary<string, EntityCondition> Conditions { get; } = new Dictionary<string, EntityCondition>();

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
            foreach (string sortField in pair.Value.Split(Chars.Separator).Select(v => v.Trim()))
            {
              if (sortField.StartsWith(Chars.Descending.ToString()))
              {
                SortFields[StringUtility.ToUpperCamelCase(sortField.Substring(1))] = SortDirection.Descending;
              }
              else
              {
                SortFields[StringUtility.ToUpperCamelCase(sortField)] = SortDirection.Ascending;
              }
            }
            break;

          case UrlKeys.FullText:
            FullText = pair.Value;
            break;

          case UrlKeys.Select:
            Includes = pair.Value
                           .Split(Chars.Separator)
                           .Select(v => v.Trim())
                           .Select(StringUtility.ToUpperCamelCase)
                           .ToArray();
            break;

          default:
            Conditions[StringUtility.ToUpperCamelCase(pair.Key)] = GetEntityCondition(pair.Value);
            break;
        }
      }
    }

    private static EntityCondition GetEntityCondition(string value)
    {
      bool isEndsWith = value.StartsWith(Chars.WildCard.ToString());
      bool isStartsWith = value.EndsWith(Chars.WildCard.ToString());
      bool isContains = isStartsWith && isEndsWith;
      bool isNotEquals = value.StartsWith(Chars.NotEqual.ToString());

      if (isContains)
      {
        return new EntityCondition(ConditionOperator.Contains, value.Trim(Chars.WildCard));
      }

      if (isStartsWith)
      {
        return new EntityCondition(ConditionOperator.BeginsWith, value.TrimEnd(Chars.WildCard));
      }

      if (isEndsWith)
      {
        return new EntityCondition(ConditionOperator.EndsWith, value.TrimStart(Chars.WildCard));
      }

      if (isNotEquals)
      {
        return new EntityCondition(ConditionOperator.NotEqual, value.TrimStart(Chars.NotEqual));
      }

      return new EntityCondition(ConditionOperator.Equal, value);
    }
  }
}