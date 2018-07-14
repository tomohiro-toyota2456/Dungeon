using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataBase : DataBase
{
  [SerializeField]
  EnemyParamBase[] enemyParams;

  public EnemyParamBase Search(int id)
  {
    for (int i = 0; i < enemyParams.Length; i++)
    {
      if (enemyParams[i].Id == id)
      {
        return enemyParams[i];
      }
    }

    return null;
  }

  public void SetData(EnemyParamBase[] data)
  {
    enemyParams = data;
  }
}
