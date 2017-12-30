/***********************************************************
 * EquipmentBaseParam
 * Author harada
 * ********************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***********************************************************
 * EquipmentBaseParam
 * 装備の基礎パラメータクラス
 * ********************************************************/
 [CreateAssetMenu(fileName = "EquipmentBaseParam",menuName ="CoreGame/EquipmentBaseParam",order = 100)]
public class EquipmentBaseParam : ScriptableObject
{
  [SerializeField]
  float minAtk;
  [SerializeField]
  float maxAtk;
  [SerializeField]
  float def;
  [SerializeField]
  int hp;
  [SerializeField]
  int durability;//耐久値　使用回数

  public float MinAtk { get { return minAtk; } set { minAtk = value; } }
  public float MaxAtk { get { return maxAtk; } set { maxAtk = value; } }
  public float Def { get { return def; } set { def = value; } }
  public int Hp { get { return hp; } set { hp = value; } }
  public int Durability { get { return durability; } set { durability = value; } }
}
