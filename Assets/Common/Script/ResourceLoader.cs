using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader
{
  static public Sprite LoadWeponIcon(int id)
  {
    string path = "WeponIcon/weponicon_" + id.ToString();
    return Resources.Load<Sprite>(path);
  }

  static public Sprite LoadArmorIcon(int id)
  {
    string path = "ArmorIcon/armoricon_" + id.ToString();
    return Resources.Load<Sprite>(path);
  }

  static public Sprite LoadEnemySprite(int id)
  {
    string path = "Enemy/enemy_" + id.ToString();
    return Resources.Load<Sprite>(path);
  }


  static public Sprite LoadDungeonBG(int id)
  {
    string path = "";

    switch(id)
    {
      case 0:
        path = "BackGround/Grassland";
        break;

      case 1:
        path = "BackGround/sandbox";
        break;

      case 2:
        path = "BackGround/Ocean";
        break;
      case 3:
        path = "BackGround/tera";
        break;
    }

    return Resources.Load<Sprite>(path);
  }
}
