using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonData : ScriptableObject
{
  [SerializeField]
  int id;
  [SerializeField]
  string dungeonName;
  [SerializeField]
  int[] appearanceTableIds;//エネミー出現テーブル

  public int Id { get { return id; } set { id = value; } }
  public string DungeonName { get { return dungeonName; } set { dungeonName = value; } }
  public int[] AppearanceTableIds { get { return appearanceTableIds; } set { appearanceTableIds = value; } }
}
