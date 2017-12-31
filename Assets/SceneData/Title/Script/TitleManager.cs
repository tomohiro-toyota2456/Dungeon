using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
  [SerializeField]
  PopupSimple simple;

  public void OpenTest()
  {
    var a = PopupManager.Instance.CreatePopup<PopupSimple>(simple);
    a.Init("a", "s","OK");
    a.Open();
  }
}
