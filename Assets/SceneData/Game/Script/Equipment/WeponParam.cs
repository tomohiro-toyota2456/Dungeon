using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeponParam",menuName ="EquipmentData/Wepon",order = 100)]
public class WeponParam : EquipmentBaseParam
{
  [SerializeField]
  float minAtk;
  [SerializeField]
  float maxAtk;
  [SerializeField]
  WeponType type;

  public enum WeponType
  {
    Main,
    Sub
  }

  public float MinAtk { get { return minAtk; } set { minAtk = value; } }
  public float MaxAtk { get { return maxAtk; } set { maxAtk = value; } }
  public WeponType Type { get { return type; } set { type = value; } }
}
