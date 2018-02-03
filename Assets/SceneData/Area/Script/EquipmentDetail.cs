using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDetail : MonoBehaviour
{
  [SerializeField]
  Button button;
  [SerializeField]
  EquipmentPopup equipmentPopup;

  PlayerEquipmentWepon main;
  PlayerEquipmentWepon sub;
  PlayerEquipmentArmor armor;

  WeponDataBase weponDataBase;
  ArmorDataBase armorDataBase;
	// Use this for initialization
	void Start ()
  {
    weponDataBase = DataBaseManager.Instance.GetDataBase<WeponDataBase>();
    armorDataBase = DataBaseManager.Instance.GetDataBase<ArmorDataBase>();

    WeponParam mainWepon, subWepon;
    ArmorParam armorParam;

    PlayerData player = new PlayerData();
    player.LoadEquipmentData();

    mainWepon = weponDataBase.Search(player.MainWepon.id, WeponParam.WeponType.Main);
    subWepon = weponDataBase.Search(player.SubWepon.id, WeponParam.WeponType.Sub);
    armorParam = armorDataBase.Search(player.Armor.id);

    main = new PlayerEquipmentWepon(WeponParam.WeponType.Main);
    main.Equip(mainWepon, player.MainWepon.options[0], player.MainWepon.options[1], player.MainWepon.options[2]);

    sub = new PlayerEquipmentWepon(WeponParam.WeponType.Sub);
    sub.Equip(subWepon, player.SubWepon.options[0], player.SubWepon.options[1], player.SubWepon.options[2]);

    armor = new PlayerEquipmentArmor();
    armor.Equip(armorParam, player.Armor.options[0], player.Armor.options[1], player.Armor.options[2]);

    button.onClick.AddListener(OpenPopup);
	}

  void OpenPopup()
  {
    var manager = PopupManager.Instance;
    var pp = manager.CreatePopup<EquipmentPopup>(equipmentPopup);
    pp.Init(main, sub, armor);
    manager.Open(pp);
  }
}
