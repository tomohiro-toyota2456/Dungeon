using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDataBase : DataBase
{
  [SerializeField]
  DungeonData[] dungeonDatas;

  public DungeonData Search(int id)
  {
    for(int i = 0; i < dungeonDatas.Length;i++)
    {
      if(dungeonDatas[i].Id == id)
      {
        return Instantiate(dungeonDatas[i]);
      }
    }

    return null;
  }
}
