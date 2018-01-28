/**********************************************************
 * PlayerEquipmentArmor
 * Author harada
 * *******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**********************************************************
 * PlayerEquipmentArmor
 * 防具制御
 * *******************************************************/
public class PlayerEquipmentArmor
{
  ArmorParam armor;
  EquipmentOptionBase[] options = new EquipmentOptionBase[GameCommon.MaxOptionNum];

  public void Equip(ArmorParam armor, EquipmentOptionBase opt1, EquipmentOptionBase opt2, EquipmentOptionBase opt3)
  {
    this.armor = armor;
    options[0] = opt1;
    options[1] = opt2;
    options[2] = opt3;
  }

  public int Id { get { return armor.Id; } }
  public int ImageId { get { return armor.ImageId; } }
  public string ArmorName { get { return armor.Name; } }

  public float CalcDef()
  {
    float def = armor.Def;
    for(int i = 0; i < options.Length; i++)
    {
      def += options[i]!=null ? options[i].Def : 0;
    }

    return def;
  }

  //最大使用回数計算
  public int CalcDurability()
  {
    int durability = armor.Durability;
    for (int i = 0; i < options.Length; i++)
    {
      durability += options[i] != null ? options[i].Durability : 0;
    }
    return durability;
  }
}
