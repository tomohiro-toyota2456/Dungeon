﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopTableDataBase : DataBase
{
  [SerializeField]
  EnemyPopTable[] tables;

  public EnemyPopTable Search(int id)
  {
    for (int i = 0; i < tables.Length; i++)
    {
      if (tables[i].Id == id)
      {
        return tables[i];
      }
    }

    return null;
  }

  public void SetData(EnemyPopTable[] table)
  {
    tables = table;
  }
}
