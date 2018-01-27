using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
  [System.Serializable]
  public struct EquipmentData
  {
    public int id;
    public EquipmentOptionBase[] options;
  }

  EquipmentData mainWepon;
  EquipmentData subWepon;
  EquipmentData armor;

  int[] imageIds = new int[3];//0:main1:sub2:armor

  string mainPath = "iheihfAFPKGeEWFf";
  string subPath = "fjeowgWeGefc";
  string armorPath = "FfFWQFwqqdqssv223";

  public void SetMainWepon(int id,EquipmentOptionBase[] data)
  {
    mainWepon.id = id;
    mainWepon.options = data;
  }

  public void SetSubWepon(int id,EquipmentOptionBase[] data)
  {
    subWepon.id = id;
    subWepon.options = data;
  }

  public void SetArmor(int id,EquipmentOptionBase[] data)
  {
    armor.id = id;
    armor.options = data;
  }

  public EquipmentData MainWepon { get { return mainWepon; } }
  public EquipmentData SubWepon { get { return subWepon; } }
  public EquipmentData Armor { get { return armor; } }

  public void SaveEquipmentData()
  {
    string wepon = JsonUtility.ToJson(mainWepon);
    string subwepon = JsonUtility.ToJson(subWepon);
    string ar = JsonUtility.ToJson(armor);

    PlayerPrefs.SetString(mainPath, wepon);
    PlayerPrefs.SetString(subPath, subwepon);
    PlayerPrefs.SetString(armorPath, ar);

    PlayerPrefs.Save();
  }

  public void LoadEquipmentData()
  {

    string main = PlayerPrefs.GetString(mainPath);
    string sub = PlayerPrefs.GetString(subPath);
    string armor = PlayerPrefs.GetString(armorPath);

    if (!string.IsNullOrEmpty(main))
    {
      mainWepon = JsonUtility.FromJson<EquipmentData>(main);
    }
    else
    {
      mainWepon.id = -1;
      mainWepon.options = new EquipmentOptionBase[3];
    }

    if(!string.IsNullOrEmpty(sub))
    {
      subWepon = JsonUtility.FromJson<EquipmentData>(sub);
    }
    else
    {
      subWepon.id = -1;
      subWepon.options = new EquipmentOptionBase[3];
    }

    if (!string.IsNullOrEmpty(armor))
    {
      this.armor = JsonUtility.FromJson<EquipmentData>(armor);
    }
    else
    {
      this.armor.id = -1;
      this.armor.options = new EquipmentOptionBase[3];
    }
  }
}
