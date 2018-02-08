using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChangerPopup : PopupBase
{
  [SerializeField]
  VolumeChanger changer;
  [SerializeField]
  Slider masterSlider;
  [SerializeField]
  Slider bgmSlider;
  [SerializeField]
  Slider seSlider;
  [SerializeField]
  Button closeButton;

	// Use this for initialization
	void Start ()
  {
    masterSlider.onValueChanged.AddListener(ChangeMasterVolume);
    bgmSlider.onValueChanged.AddListener(ChangeBgmVolume);
    seSlider.onValueChanged.AddListener(ChangeSeVolume);

    closeButton.onClick.AddListener(Close);
	}
	
  void ChangeMasterVolume(float val)
  {
    changer.SetMasterVolume(val);
  }

  void ChangeBgmVolume(float val)
  {
    changer.SetBgmVolume(val);
  }

  void ChangeSeVolume(float val)
  {
    changer.SetSeVolume(val);
  }
}
