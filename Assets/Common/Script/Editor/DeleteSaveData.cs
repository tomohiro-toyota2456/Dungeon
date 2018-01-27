using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeleteSaveData
{
  [MenuItem("SaveData/Delete")]
  static void Delete()
  {
    PlayerPrefs.DeleteAll();
  }
}
