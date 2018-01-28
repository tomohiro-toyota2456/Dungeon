using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquipmentChangePopup : PopupBase
{
  [SerializeField]
  TextMeshProUGUI paramTitleText;
  [SerializeField]
  TextMeshProUGUI paramText;
  [SerializeField]
  TextMeshProUGUI ctText;
  [SerializeField]
  TextMeshProUGUI duraText;
  [SerializeField]
  TextMeshProUGUI paramTitleTextB;
  [SerializeField]
  TextMeshProUGUI paramTextB;
  [SerializeField]
  TextMeshProUGUI ctTextB;
  [SerializeField]
  TextMeshProUGUI duraTextB;
  [SerializeField]
  TextMeshProUGUI nameTextA;
  [SerializeField]
  TextMeshProUGUI nameTextB;

  [SerializeField]
  Image icon1;
  [SerializeField]
  Image icon2;
  [SerializeField]
  Button button1;
  [SerializeField]
  Button button2;
  [SerializeField]
  Sprite[] onOffSprites = new Sprite[2];
  [SerializeField]
  Button decisionButton;

  bool isSelectedNowEquipment = true;//現在装備選択中
  public bool IsSelectedNowEquipment { get { return isSelectedNowEquipment; } }

  bool isDecision = false;
  public enum EquipmentType
  {
    Wepon,
    Armor
  }

  //paramは防具なら防御力　武器なら攻撃力を入れる　Aは装備中のもの Bはドロップのものを入れる
  public void Init(EquipmentType type,int imageIdA,string nameA,float paramA,int criticalA,int duraParamA,int imageIdB,string nameB,float paramB,int criticalB,int duraParamB)
  {
    SetTitleText(type);
    paramText.text = paramA.ToString();
    ctText.text = criticalA.ToString();
    duraText.text = duraParamA.ToString();
    nameTextA.text = nameA;


    paramTextB.text = paramB.ToString();
    ctTextB.text = criticalB.ToString();
    duraTextB.text = duraParamB.ToString();
    nameTextB.text = nameB;
    UpdateButtonSprite();

    button1.onClick.RemoveAllListeners();
    button2.onClick.RemoveAllListeners();
    button1.onClick.AddListener(()=>OnClickEquipmentButton(true));
    button2.onClick.AddListener(()=>OnClickEquipmentButton(false));

    decisionButton.onClick.RemoveAllListeners();
    decisionButton.onClick.AddListener(OnClickDecisionButton);
  }

  void SetTitleText(EquipmentType type)
  {
    paramTitleText.text = type == EquipmentType.Armor ? "防御力" : "攻撃力";
    paramTitleTextB.text = paramTitleText.text;
  }

  void OnClickEquipmentButton(bool isNowSelect)
  {
    isSelectedNowEquipment = isNowSelect;
    UpdateButtonSprite();
  }

  void UpdateButtonSprite()
  {
    button1.image.sprite = isSelectedNowEquipment ? onOffSprites[0] : onOffSprites[1];
    button2.image.sprite = isSelectedNowEquipment ? onOffSprites[1] : onOffSprites[0];
  }

  void OnClickDecisionButton()
  {
    isDecision = true;
    Close();
  }

  public IEnumerator WaitDecision()
  {
    while (!isDecision)
      yield return null;
  }
}
