/**********************************************************
 * GaugeUI.cs
 * Author Harada
 * *******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**********************************************************
 * GaugeUI
 * UIでゲージ表示
 * *******************************************************/
public class GaugeUI : MonoBehaviour
{
  [SerializeField]
  Image gaugeImage;//動くほうを指定する
  [SerializeField]
  TextMeshProUGUI numberText;

  public void SetNumber(int number,int maxNumber,bool isShowNumber = true)
  {
    float scl = (float)number / (float)maxNumber;
    Vector3 vec3 = new Vector3(scl, 1, 1);
    gaugeImage.transform.localScale = vec3;

    numberText.gameObject.SetActive(isShowNumber);
    if(isShowNumber)
    {
      numberText.text = number.ToString() + "/" + maxNumber.ToString();
    }
  }
}
