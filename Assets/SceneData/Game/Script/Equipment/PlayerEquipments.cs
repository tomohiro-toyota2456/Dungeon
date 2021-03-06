﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipments
{
  PlayerEquipmentWepon mainWepon;
  PlayerEquipmentWepon subWepon;
  PlayerEquipmentArmor armor;

  static readonly int EquipmentNum = 3;//main sub armorの順

  int[] equipmentUsageCounts = new int[EquipmentNum];//装備数分 使用回数 使用回数 = 使用可能回数で壊れる

  //使用可能回数
  public int enableUsingCountMainWepon { get { return mainWepon.CalcDurability() - equipmentUsageCounts[0]; } }
  public int enableUsingCountSubWepon { get { return subWepon.CalcDurability() - equipmentUsageCounts[1]; } }
  public int enableUsingCountArmor { get { return armor.CalcDurability() - equipmentUsageCounts[2]; } }

  public WeponParam.EffectType MainWeponEffectType { get { return mainWepon.EffectType; } }
  public WeponParam.EffectType SubWeponEffctType { get { return subWepon.EffectType; } }

  public string ArmorName { get { return armor.ArmorName; } }
  public string MainWeponName { get { return mainWepon.WeponName; } }
  public string SubWeponName { get { return subWepon.WeponName; } }

  public int[] GetEquipmentIds()
  {
    int[] ids = new int[EquipmentNum];
    ids[0] = mainWepon.Id;
    ids[1] = subWepon.Id;
    ids[2] = armor.Id;
    return ids;
  }

  public int[] GetEquipmentImageIds()
  {
    int[] ids = new int[EquipmentNum];
    ids[0] = mainWepon.ImageId;
    ids[1] = subWepon.ImageId;
    ids[2] = armor.ImageId;
    return ids;
  }

  public PlayerEquipments()
  {
    mainWepon = new PlayerEquipmentWepon(WeponParam.WeponType.Main);
    subWepon = new PlayerEquipmentWepon(WeponParam.WeponType.Sub);
    armor = new PlayerEquipmentArmor();
    for(int i = 0; i < equipmentUsageCounts.Length;i++)
    {
      equipmentUsageCounts[i] = 0;
    }
  }

  public void ResetEnableUsingCount()
  {
    for (int i = 0; i < equipmentUsageCounts.Length; i++)
    {
      equipmentUsageCounts[i] = 0;
    }
  }

  public void SetMainWepon(WeponParam wepon, EquipmentOptionBase opt1, EquipmentOptionBase opt2, EquipmentOptionBase opt3)
  {
    equipmentUsageCounts[0] = 0;
    mainWepon.Equip(wepon, opt1, opt2, opt3);
  }

  public void SetSubWepon(WeponParam wepon, EquipmentOptionBase opt1, EquipmentOptionBase opt2, EquipmentOptionBase opt3)
  {
    equipmentUsageCounts[1] = 0;
    subWepon.Equip(wepon, opt1, opt2, opt3);
  }

  public void SetArmor(ArmorParam armor, EquipmentOptionBase opt1, EquipmentOptionBase opt2, EquipmentOptionBase opt3)
  {
    equipmentUsageCounts[2] = 0;
    this.armor.Equip(armor, opt1, opt2, opt3);
  }

  //武器使用関数
  public void UseMainWepon()
  {
    equipmentUsageCounts[0]++;
  }

  public void UseSubWepon()
  {
    equipmentUsageCounts[1]++;
  }

  //防具使用関数
  public void UseArmor()
  {
    equipmentUsageCounts[2]++;
  }

  public float CalcMainWeponAtk()
  {
    return mainWepon.CalcAtkRandomMinToMax();
  }

  public float CalcSubWeponAtk()
  {
    return subWepon.CalcAtkRandomMinToMax();
  }

  public int CalcMainWeponCritical()
  {
    return mainWepon.CalcCritical();
  }

  public int CalcSubWeponCritical()
  {
    return subWepon.CalcCritical();
  }

  public float CalcDef()
  {
    return armor.CalcDef();
  }

  public float CalcMainWeponMaxAtk()
  {
    return mainWepon.CalcMaxAtk();
  }

  public float CalcSubWeponMaxAtk()
  {
    return subWepon.CalcMaxAtk();
  }

}
