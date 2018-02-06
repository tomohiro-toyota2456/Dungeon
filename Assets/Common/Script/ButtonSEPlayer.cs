using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSEPlayer : MonoBehaviour
{
  [SerializeField]
  Button button;
  [SerializeField]
  ButtonType type;

  public enum ButtonType
  {
    Ok,
    Cancel
  }

	// Use this for initialization
	void Start ()
  {
    if (button != null)
      button = GetComponent<Button>();

    switch(type)
    {
      case ButtonType.Ok:
        button.onClick.AddListener(() => SoundPlayer.Instance.PlaySe(GameCommon.buttonOk));
        break;

      case ButtonType.Cancel:
        button.onClick.AddListener(() => SoundPlayer.Instance.PlaySe(GameCommon.buttonCancel));
        break;
    }
	}
}
