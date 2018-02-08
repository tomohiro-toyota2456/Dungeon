using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChangeButton : MonoBehaviour
{
  [SerializeField]
  Button button;
  [SerializeField]
  VolumeChangerPopup popup;

  private void Start()
  {
    button.onClick.AddListener(OpenVolumeChangePopup);
  }

  void OpenVolumeChangePopup()
  {
    var pp = PopupManager.Instance.CreatePopup<VolumeChangerPopup>(popup);

    PopupManager.Instance.Open(pp);
  }
}
