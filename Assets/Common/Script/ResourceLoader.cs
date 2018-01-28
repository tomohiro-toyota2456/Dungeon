using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader
{
  static public Sprite LoadWeponIcon(int id)
  {
    string path = "Resources/WeponIcon/weponicon_" + id.ToString();
    return Resources.Load<Sprite>(path);
  }

  static public Sprite LoadArmorIcon(int id)
  {
    string path = "Resources/ArmorIcon/armoricon_" + id.ToString();
    return Resources.Load<Sprite>(path);
  }

  static public Sprite LoadEnemySprite(int id)
  {
    string path = "Resources/Enemy/enemy_" + id.ToString();
    return Resources.Load<Sprite>(path);
  }
}
