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
  [SerializeField]
  int minAtkOp;
  [SerializeField]
  int maxAtkOp;
  [SerializeField]
  int minDefOp;
  [SerializeField]
  int maxDefOp;
  [SerializeField]
  int minCtOp;
  [SerializeField]
  int maxCtOp;
  [SerializeField]
  int minDura;
  [SerializeField]
  int maxDura;

  public int Id { get { return id; } set { id = value; } }
  public string DungeonName { get { return dungeonName; } set { dungeonName = value; } }
  public int[] AppearanceTableIds { get { return appearanceTableIds; } set { appearanceTableIds = value; } }
  public int MinAtkOp { get { return minAtkOp; } set { minAtkOp = value; } }
  public int MaxAtkOp { get { return maxAtkOp; } set { maxAtkOp = value; } }
  public int MinDefOp { get { return minDefOp; } set { minDefOp = value; } }
  public int MaxDefOp { get { return maxDefOp; } set { maxDefOp = value; } }
  public int MinCtOp { get { return minCtOp; } set { minCtOp = value; } }
  public int MaxCtOp { get { return maxCtOp; } set { maxCtOp = value; } }
  public int MinDura { get { return minDura; } set { minDura = value; } }
  public int MaxDura { get { return maxDura; } set { maxDura = value; } }
}
