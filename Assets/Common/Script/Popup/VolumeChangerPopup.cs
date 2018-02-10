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

    masterSlider.value = ConvertInVal(changer.GetMasterVolume());
    bgmSlider.value = ConvertInVal(changer.GetBgmVolume());
    seSlider.value = ConvertInVal(changer.GetSeVolume());

    closeButton.onClick.AddListener(Close);
	}
	
  void ChangeMasterVolume(float val)
  {
    changer.SetMasterVolume(ConvertDB(val));
  }

  void ChangeBgmVolume(float val)
  {
    changer.SetBgmVolume(ConvertDB(val));
  }

  void ChangeSeVolume(float val)
  {
    changer.SetSeVolume(ConvertDB(val));
  }

  float ConvertDB(float val)
  {
    float db = 20 * Mathf.Log10(val);
    return Mathf.Clamp(db, -80, 0);
  }

  float ConvertInVal(float val)
  {
    return Mathf.Clamp(Mathf.Pow(10, val / 20),0.0001f,1.0f);
  }
}
