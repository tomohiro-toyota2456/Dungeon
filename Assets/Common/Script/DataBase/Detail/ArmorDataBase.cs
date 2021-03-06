﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorDataBase : DataBase
{
  [SerializeField]
  ArmorParam[] armorParams;

  public ArmorParam Search(int id)
  {
    if (id == -1)
      return GetDefaultArmor();

    for (int i = 0; i < armorParams.Length; i++)
    {
      if (armorParams[i].Id == id)
      {
        return armorParams[i];
      }
    }

    return null;
  }

  public ArmorParam GetDefaultArmor()
  {
    var armor = ScriptableObject.CreateInstance<ArmorParam>();
    armor.Def = 1;
    armor.Durability = 99;
    armor.Id = -1;
    armor.ImageId = 0;
    armor.Name = "ぬののふく";
    return armor;
  }

  public void SetData(ArmorParam[] data)
  {
    armorParams = data;
  }

}
