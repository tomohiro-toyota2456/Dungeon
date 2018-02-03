using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParam
{
  PlayerEquipments equipments = new PlayerEquipments();
  int maxHp = GameCommon.PlayerHp;
  int curHp;
  int enableUsingCountRepair = GameCommon.EnableRepairNum;

  public int EnableUsingCountMainWepon { get { return equipments.enableUsingCountMainWepon; } }
  public int EnableUsingCountSubWepon { get { return equipments.enableUsingCountSubWepon; } }
  public int EnableUsingCountArmor { get { return equipments.enableUsingCountArmor; } }
  public int EnableUsingCountRepair { get { return enableUsingCountRepair; } }
  public int MaxHp { get { return maxHp; } }
  public int CurHp { get { return curHp; } }

  public WeponParam.EffectType MainWeponEffectType { get { return equipments.MainWeponEffectType; } }
  public WeponParam.EffectType SubWeponEffectType { get { return equipments.SubWeponEffctType; } }

  public string MainWeponName { get { return equipments.MainWeponName; } }
  public string SubWeponName { get { return equipments.SubWeponName; } }
  public string ArmorName { get { return equipments.ArmorName; } }

  public int[] GetEquipmentIds()
  {
    return equipments.GetEquipmentIds();
  }

  public int[] GetEquipmentImageIds()
  {
    return equipments.GetEquipmentImageIds();
  }

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

  public void SetMainWepon(WeponParam wepon,EquipmentOptionBase[] options)
  {
    equipments.SetMainWepon(wepon, options[0], options[1], options[2]);
  }

  public void SetSubWepon(WeponParam wepon, EquipmentOptionBase[] options)
  {
    equipments.SetSubWepon(wepon, options[0], options[1], options[2]);
  }

  public void SetArmor(ArmorParam armor, EquipmentOptionBase[] options)
  {
    equipments.SetArmor(armor, options[0], options[1], options[2]);
  }

  public void SetMainWepon(WeponParam wepon,EquipmentOptionBase opt1, EquipmentOptionBase opt2, EquipmentOptionBase opt3)
  {
    equipments.SetMainWepon(wepon, opt1, opt2, opt3);
  }

  public void SetSubWepon(WeponParam wepon, EquipmentOptionBase opt1, EquipmentOptionBase opt2, EquipmentOptionBase opt3)
  {
    equipments.SetSubWepon(wepon, opt1, opt2, opt3);
  }

  public void SetArmor(ArmorParam armor, EquipmentOptionBase opt1, EquipmentOptionBase opt2, EquipmentOptionBase opt3)
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

  public void UserRepair()
  {
    curHp = MaxHp;
    enableUsingCountRepair--;
  }

  public void Damage(float damage)
  {
    curHp -= (int)damage;
    curHp = curHp < 0 ? 0 : curHp;
  }

  public int CalcMainWeponCritical()
  {
    return equipments.CalcMainWeponCritical();
  }

  public int CalcSubWeponCritical()
  {
    return equipments.CalcSubWeponCritical();
  }

  public float CalcMainWeponMaxAtk()
  {
    return equipments.CalcMainWeponMaxAtk();
  }

  public float CalcSubWeponMaxAtk()
  {
    return equipments.CalcSubWeponMaxAtk();
  }

  public float CalcArmorDef()
  {
    return equipments.CalcDef();
  }

}
