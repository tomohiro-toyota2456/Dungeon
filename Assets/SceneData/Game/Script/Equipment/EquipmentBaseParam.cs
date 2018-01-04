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
public abstract class EquipmentBaseParam : ScriptableObject
{
  [SerializeField]
  int id;
  [SerializeField]
  int imageId;
  [SerializeField]
  string equipmentName;
  [SerializeField]
  int durability;//耐久値　使用回数

  public int Id { get { return id; } set { id = value; } }
  public int ImageId { get { return imageId; } set { imageId = value; } }
  public string Name { get { return equipmentName; } set { equipmentName = value; } }
  public int Durability { get { return durability; } set { durability = value; } }
}
