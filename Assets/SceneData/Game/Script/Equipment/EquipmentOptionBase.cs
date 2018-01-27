/***********************************************************
 * EquipmentOptionBase
 * Author harada
 * ********************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***********************************************************
 * EquipmentBaseParam
 * 装備Optionの基礎パラメータクラス
 * ********************************************************/
 [System.Serializable]
public class EquipmentOptionBase 
{
  [SerializeField]
  string name;
  [SerializeField]
  float atk;
  [SerializeField]
  float def;
  [SerializeField]
  int durability;
  [SerializeField]
  int critical;//%単位

  public string Name { get { return name; } set { name = value; } }
  public float Atk { get { return atk; } set { atk = value; } }
  public float Def { get { return def; } set { def = value; } }
  public int Durability { get { return durability; } set { durability = value; } }
  public int Critical { get { return critical; } set { critical = value; } }
}
