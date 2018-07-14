using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDataBase : DataBase
{
  [SerializeField]
  DungeonData[] dungeonDatas;

  public struct ShortData
  {
    public int id;
    public string name;
  }

  public DungeonData Search(int id)
  {
    for(int i = 0; i < dungeonDatas.Length;i++)
    {
      if(dungeonDatas[i].Id == id)
      {
        return dungeonDatas[i];
      }
    }

    return null;
  }

  public ShortData[] GetShortDatas()
  {
    ShortData[] shortDatas = new ShortData[4];//ダミー
    for(int i = 0; i < shortDatas.Length; i++)
    {
      if(i < dungeonDatas.Length )
      {
        shortDatas[i].id = dungeonDatas[i].Id;
        shortDatas[i].name = dungeonDatas[i].DungeonName;
      }
    }

    return shortDatas;
  }

  public void SetData(DungeonData[] datas)
  {
    dungeonDatas = datas;
  }
}
