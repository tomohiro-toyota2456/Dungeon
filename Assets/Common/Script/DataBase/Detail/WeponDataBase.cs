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

  public WeponParam GetDefaultWepon(WeponParam.WeponType type)
  {
    var wepon = ScriptableObject.CreateInstance<WeponParam>();
    wepon.MinAtk = 1;
    wepon.MaxAtk = 5;
    wepon.Id = -1;
    wepon.ImageId = -1;
    wepon.Name = "はがねのこぶし";
    wepon.Durability = 99;
    wepon.Type = type;
    return wepon;
  }

  public void SetData(WeponParam[] param)
  {
    weponParams = param;
  }
}
