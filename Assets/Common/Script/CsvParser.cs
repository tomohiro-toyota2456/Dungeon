using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public static class CsvParser
{
  public static string[] Parse(string csv)
  {
    List<string> list = new List<string>();

    int stIdx = 0;
    while(true)
    {
      int hitIdx = csv.IndexOf(",",stIdx);

      if(hitIdx == -1)
      {
        list.Add(csv.Substring(stIdx, csv.Length - 1 - stIdx));
        break;
      }

      list.Add(csv.Substring(stIdx, hitIdx - stIdx));
    }

    return list.ToArray();
  }

  public static int[] ParseInt(string csv)
  {
    if(string.IsNullOrEmpty(csv))
    {
      return null;
    }

    List<string> list = new List<string>();

    int stIdx = 0;
    while (true)
    {
      int hitIdx = csv.IndexOf(",", stIdx);

      if (hitIdx == -1)
      {
        list.Add(csv.Substring(stIdx, csv.Length - stIdx));
        break;
      }

      list.Add(csv.Substring(stIdx, hitIdx - stIdx));
      stIdx = hitIdx + 1;
    }

    return list.Select(str => int.Parse(str)).ToArray();
  }
}
