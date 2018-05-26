using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
  public void MoveAreaMap()
  {
    ChangeScene.Instance.LoadScene("AreaMap");
  }
}
