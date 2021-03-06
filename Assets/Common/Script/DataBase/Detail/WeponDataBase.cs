﻿using System.Collections;
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
        return weponParams[i];
      }
    }

    return null;
  }

  public WeponParam Search(int id,WeponParam.WeponType type)
  {
    if(id == -1)
    {
      return GetDefaultWepon(type);
    }

    var data = Search(id);

    if(data.Type != type)
    {
    }

    return data;
  }

  public WeponParam GetDefaultWepon(WeponParam.WeponType type)
  {
    var wepon = ScriptableObject.CreateInstance<WeponParam>();
    wepon.MinAtk = 1;
    wepon.MaxAtk = 6;
    wepon.Id = -1;
    wepon.Critical = 5;
    wepon.Name = "はがねのこぶし";
    wepon.Durability = 99;
    wepon.Type = type;
    wepon.ImageId = 1;
    wepon.eType = WeponParam.EffectType.Striking;
    return wepon;
  }

  public void SetData(WeponParam[] param)
  {
    weponParams = param;
  }
}
