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

  public int UseMainWepon(float enemyDef)
  {
    int damage = (int)equipments.UseMainWepon(enemyDef);
    return damage;
  }

  public int UseSubWepon(float enemyDef)
  {
    int damage = (int)equipments.UseSubWepon(enemyDef);
    return damage;
  }

  public int UseArmor(float enemyAtk,int enemyCriticalPoint)
  {
    int damage = (int)equipments.UseArmor(enemyAtk, enemyCriticalPoint);
    curHp -= damage;
    return damage;
  }

}
