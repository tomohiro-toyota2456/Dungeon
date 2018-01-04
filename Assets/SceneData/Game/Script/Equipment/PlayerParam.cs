using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParam : MonoBehaviour
{
  PlayerEquipments equipments = new PlayerEquipments();
  int maxHp = GameCommon.PlayerHp;
  int curHp;

  public int EnableUsingCountMainWepon { get { return equipments.enableUsingCountMainWepon; } }
  public int EnableUsingCountSubWepon { get { return equipments.enableUsingCountSubWepon; } }
  public int EnableUsingCountArmor { get { return equipments.enableUsingCountArmor; } }
  public int MaxHp { get { return maxHp; } }
  public int CurHp { get { return curHp; } }

  public WeponParam.EffectType MainWeponEffectType { get { return equipments.MainWeponEffectType; } }
  public WeponParam.EffectType SubWeponEffectType { get { return equipments.SubWeponEffctType; } }

  public struct ParamData
  {
    public ParamData(float atk,int cri)
    {
      this.atk = atk;
      critical = cri;
    }
    public float atk;
    public int critical;
  }

  public void Init()
  {
    curHp = maxHp;
  }

  public void SetMainWepon(WeponParam wepon,EquipmentOptionBase opt1, EquipmentOptionBase opt2, EquipmentOptionBase opt3)
  {
    equipments.SetMainWepon(wepon, opt1, opt2, opt3);
  }

  public void SetSubWepon(WeponParam wepon, EquipmentOptionBase opt1, EquipmentOptionBase opt2, EquipmentOptionBase opt3)
  {
    equipments.SetSubWepon(wepon, opt1, opt2, opt3);
  }

  public void SetArmorWepon(ArmorParam armor, EquipmentOptionBase opt1, EquipmentOptionBase opt2, EquipmentOptionBase opt3)
  {
    equipments.SetArmor(armor, opt1, opt2, opt3);
  }

  public ParamData UseMainWepon()
  {
    equipments.UseMainWepon();
    ParamData paramData = new ParamData(equipments.CalcMainWeponAtk(), equipments.CalcMainWeponCritical());
    return paramData;
  }

  public ParamData UseSubWepon()
  {
    equipments.UseSubWepon();
    ParamData paramData = new ParamData(equipments.CalcSubWeponAtk(), equipments.CalcSubWeponCritical());
    return paramData;
  }

  public float UseArmor()
  {
    equipments.UseArmor();
    return equipments.CalcDef();
  }

  public void Damage(float damage)
  {
    curHp -= (int)damage;
  }

}
