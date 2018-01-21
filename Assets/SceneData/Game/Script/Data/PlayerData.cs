using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
  public struct EquipmentData
  {
    public int id;
    public EquipmentOptionBase[] options;
  }

  EquipmentData mainWepon;
  EquipmentData subWepon;
  EquipmentData armor;

  int[] imageIds = new int[3];//0:main1:sub2:armor

  public void SetMainWepon(int id,EquipmentOptionBase[] data)
  {
    mainWepon.id = id;
    mainWepon.options = data;
  }

  public void SetSubWepon(int id,EquipmentOptionBase[] data)
  {
    subWepon.id = id;
    subWepon.options = data;
  }

  public void SetArmor(int id,EquipmentOptionBase[] data)
  {
    armor.id = id;
    armor.options = data;
  }

  public EquipmentData MainWepon { get { return mainWepon; } }
  public EquipmentData SubWepon { get { return subWepon; } }
  public EquipmentData Armor { get { return armor; } }
}
