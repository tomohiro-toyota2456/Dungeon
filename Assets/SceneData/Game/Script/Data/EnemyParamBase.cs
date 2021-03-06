﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParamBase : ScriptableObject
{
  [SerializeField]
  int id;
  [SerializeField]
  int imageId;
  [SerializeField]
  string enemyName;
  [SerializeField]
  int dropTableId;
  [SerializeField]
  int maxHp;
  [SerializeField]
  float maxAtk;
  [SerializeField]
  float minAtk;
  [SerializeField]
  float def;
  [SerializeField]
  int critical;
  [SerializeField]
  int dropPer;
  [SerializeField]
  EnemyType enemyType;

  public enum EnemyType
  {
    Monster,
    Object
  } 

  public int Id { get { return id; } set { id = value; } }
  public int ImageId { get { return imageId; } set { imageId = value; } }
  public string EnemyName { get { return enemyName; } set { enemyName = value; } }
  public int DropTableId { get { return dropTableId; } set { dropTableId = value; } }
  public int MaxHp { get { return maxHp; } set { maxHp = value; } }
  public float MaxAtk { get { return maxAtk; } set { maxAtk = value; } }
  public float MinAtk { get { return minAtk; } set { minAtk = value; } }
  public float Def { get { return def; } set { def = value; } }
  public int Critical { get { return critical; } set { critical = value; } }
  public int DropPer { get { return dropPer; } set { dropPer = value; } }
  public EnemyType Type { get { return enemyType; } set { enemyType = value; } }
}
