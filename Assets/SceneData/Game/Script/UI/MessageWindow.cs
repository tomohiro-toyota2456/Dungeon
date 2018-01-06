using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MessageWindow : MonoBehaviour
{
  [SerializeField]
  TextMeshProUGUI messageText;
  [SerializeField]
  GameObject messageWindow;//ベース部分　ここを消すとすべて消えるような構成にする

  public void Show()
  {
    messageWindow.SetActive(true);
  }

  public void Hide()
  {
    messageWindow.SetActive(false);
  }

  public void SetMessage(string str)
  {
    messageText.text = str;
  }

  public IEnumerator ShowMessageOneCharacterAtTimeCoroutine(string str,float time)
  {
    messageText.text = str;
    messageText.maxVisibleCharacters = 0;

    int len = str.Length;
    float oneTime = time / (float)len;

    float timer = 0;
    while(timer <= time)
    {
      int t = (int)(timer / oneTime);
      int count = t % (len+1);
      messageText.maxVisibleCharacters = count;
      timer += Time.deltaTime;
      yield return null;
    }

    messageText.maxVisibleCharacters = str.Length;
  }

}
