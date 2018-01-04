using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleCommandButton : Button
{
  [SerializeField]
  TextMeshProUGUI numberText;
  [SerializeField]
  GameObject numberBg;

  public void SetNumber(int num)
  {
    numberText.gameObject.SetActive(true);
    numberBg.SetActive(true);
    numberText.text = num.ToString();
  }
  
}
