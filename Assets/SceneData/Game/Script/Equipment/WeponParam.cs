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
  int critical;
  [SerializeField]
  EffectType etype;
  [SerializeField]
  WeponType type;

  public enum WeponType
  {
    Main,
    Sub
  }

  public enum EffectType
  {
    Slashing,//斬撃
    Striking//打撃
  }

  public float MinAtk { get { return minAtk; } set { minAtk = value; } }
  public float MaxAtk { get { return maxAtk; } set { maxAtk = value; } }
  public int Critical { get { return critical; } set { critical = value; } }
  public WeponType Type { get { return type; } set { type = value; } }
  public EffectType eType { get { return etype; } set { etype = value; } }

}
