using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : ScriptableObject
{
  [SerializeField]
  int id;
  [SerializeField]
  Data[] data;

  public enum DropType
  {
    Wepon,
    Armor
  }

  [System.Serializable]
  public struct Data
  {
    public DropType dropType;
    public int id;
  }

  public int Id { get { return id; } set { id = value; } }
  public Data[] DropData { get { return data; } set { data = value; } }

  public Data GetRandom()
  {
    int idx = Random.Range(0, data.Length);
    return data[idx];
  }
}
