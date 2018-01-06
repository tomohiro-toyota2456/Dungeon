using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorDataBase : DataBase
{
  [SerializeField]
  ArmorParam[] armorParams;

  public ArmorParam Search(int id)
  {
    for (int i = 0; i < armorParams.Length; i++)
    {
      if (armorParams[i].Id == id)
      {
        return Instantiate(armorParams[i]);
      }
    }

    return null;
  }
}
