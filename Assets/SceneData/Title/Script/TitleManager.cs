using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
  private void Start()
  {
    SoundPlayer.Instance.PlayBgmCrossFade("BGM/Title");
  }

  public void MoveAreaMap()
  {
    ChangeScene.Instance.LoadScene("AreaMap");
  }
}
