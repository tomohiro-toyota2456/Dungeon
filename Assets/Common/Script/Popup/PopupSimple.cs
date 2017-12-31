using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PopupSimple : PopupBase
{
  [SerializeField]
  TextMeshProUGUI titleText;
  [SerializeField]
  TextMeshProUGUI descriptionText;
  [SerializeField]
  Button buttonA;
  [SerializeField]
  Button buttonB;

  public void Init(string title,string description, string okButtonStr = "OK", Action okAction = null)
  {
    buttonB.gameObject.SetActive(false);
    buttonA.onClick.AddListener(()=>Close());

    if (okAction != null)
    {
      buttonA.onClick.AddListener(() => okAction());
    }

    SetText(title, description);
  }

  public void Init(string title, string description, Action yesAction = null, Action noAction = null,string yesButtonStr ="Yes",string noButtonStr = "No")
  {
    if (yesAction != null)
    {
      buttonA.onClick.AddListener(() => yesAction());
    }

    if (noAction != null)
    {
      buttonB.onClick.AddListener(() => noAction());
    }

    buttonA.onClick.AddListener(() => Close());
    buttonB.onClick.AddListener(() => Close());

    SetText(title, description);
  }

  void SetText(string title, string description)
  {
    titleText.text = title;
    descriptionText.text = description;
  }

}
