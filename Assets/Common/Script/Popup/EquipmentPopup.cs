﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentPopup : PopupBase
{
  [SerializeField]
  TextMeshProUGUI mainWeponNameText;
  [SerializeField]
  TextMeshProUGUI subWeponNameText;
  [SerializeField]
  TextMeshProUGUI armorNameText;
  [SerializeField]
  TextMeshProUGUI mainWeponAtkText;
  [SerializeField]
  TextMeshProUGUI mainWeponCriticalText;
  [SerializeField]
  TextMeshProUGUI mainWeponDuraText;
  [SerializeField]
  TextMeshProUGUI subWeponAtkText;
  [SerializeField]
  TextMeshProUGUI subWeponCriticalText;
  [SerializeField]
  TextMeshProUGUI subWeponDuraText;
  [SerializeField]
  TextMeshProUGUI armorDefText;
  [SerializeField]
  TextMeshProUGUI armorCriticalText;
  [SerializeField]
  TextMeshProUGUI armorDuraText;
  [SerializeField]
  Image mainWeponIcon;
  [SerializeField]
  Image subWeponIcon;
  [SerializeField]
  Image armorIcon;
  [SerializeField]
  Button okButton;

  private void Start()
  {
    okButton.onClick.AddListener(() => Close());
  }

  public void Init(PlayerParam playerParam)
  {
    SetMainWeponParam(playerParam.CalcMainWeponMaxAtk(), playerParam.CalcMainWeponCritical(), playerParam.EnableUsingCountMainWepon);
    SetSubWeponParam(playerParam.CalcSubWeponMaxAtk(), playerParam.CalcSubWeponCritical(), playerParam.EnableUsingCountSubWepon);
    SetArmorParam(playerParam.CalcArmorDef(),playerParam.EnableUsingCountArmor);

    mainWeponNameText.text = playerParam.MainWeponName +"(メイン)";
    subWeponNameText.text  = playerParam.SubWeponName+"(サブ)";
    armorNameText.text     = playerParam.ArmorName;

    int[] imageIds = playerParam.GetEquipmentImageIds();

    mainWeponIcon.sprite = ResourceLoader.LoadWeponIcon(imageIds[0]);
    subWeponIcon.sprite =  ResourceLoader.LoadWeponIcon(imageIds[1]);
    armorIcon.sprite =     ResourceLoader.LoadArmorIcon(imageIds[2]);
  }

  public void Init(PlayerEquipmentWepon main,PlayerEquipmentWepon sub,PlayerEquipmentArmor armor)
  {
    SetMainWeponParam(main.CalcMaxAtk(), main.CalcCritical(), main.CalcDurability());
    SetSubWeponParam(sub.CalcMaxAtk(), sub.CalcCritical(), sub.CalcDurability());
    SetArmorParam(armor.CalcDef(), armor.CalcDurability());

    mainWeponNameText.text = main.WeponName;
    subWeponNameText.text  = sub.WeponName;
    armorNameText.text     = armor.ArmorName;

    mainWeponIcon.sprite = ResourceLoader.LoadWeponIcon(main.ImageId);
    subWeponIcon.sprite = ResourceLoader.LoadWeponIcon(sub.ImageId);
    armorIcon.sprite = ResourceLoader.LoadArmorIcon(armor.ImageId);

  }

  void SetMainWeponParam(float atk,int critical,int dura)
  {
    mainWeponAtkText.text = atk.ToString();
    mainWeponCriticalText.text = critical.ToString();
    mainWeponDuraText.text = dura.ToString();
  }

  void SetSubWeponParam(float atk,int critical,int dura)
  {
    subWeponAtkText.text = atk.ToString();
    subWeponCriticalText.text = critical.ToString();
    subWeponDuraText.text = dura.ToString();
  }

  void SetArmorParam(float def,int dura)
  {
    armorDefText.text = def.ToString();
    armorCriticalText.text = "0";
    armorDuraText.text = dura.ToString();
  }

}
