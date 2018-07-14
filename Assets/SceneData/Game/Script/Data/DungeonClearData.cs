using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DungeonClearData 
{
  static string key = "jwj0ueeeWQWrLVQCCXX";

  public struct ClearData
  {
    public bool[] isClears;
  }

  public static ClearData LoadClearData(int maxStage)
  {
    ClearData clearData;

    string json = PlayerPrefs.GetString(key);

    if (!string.IsNullOrEmpty(json))
    {
      clearData = JsonUtility.FromJson<ClearData>(json);
    }
    else
    {
      clearData.isClears = new bool[maxStage];
    }

    return clearData;
  }

  public static void SaveClearData(ClearData clearData)
  {
    string json = JsonUtility.ToJson(clearData);

    PlayerPrefs.SetString(key, json);
  }
}
