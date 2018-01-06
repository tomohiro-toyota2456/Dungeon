using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponDataBase : DataBase
{
  [SerializeField]
  WeponParam[] weponParams;

  public WeponParam Search(int id)
  {
    for(int i = 0; i < weponParams.Length; i++)
    {
      if(weponParams[i].Id == id)
      {
        return Instantiate(weponParams[i]);
      }
    }

    return null;
  }
}
