using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmorParam", menuName = "EquipmentData/Armor", order = 100)]
public class ArmorParam : EquipmentBaseParam
{
  [SerializeField]
  float def;

  public float Def { get { return def; } set { def = value; } }
}
