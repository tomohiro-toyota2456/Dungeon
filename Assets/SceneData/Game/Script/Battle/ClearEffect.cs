using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClearEffect : ClearEffectBase
{
  [SerializeField]
  RectTransform[] clearImages;//一文字ずつ分解したもの
  [SerializeField]
  GameObject root;

  public override IEnumerator PlayEffect()
  {
    root.SetActive(true);
    for(int i = 0; i < clearImages.Length; i++)
    {
      PlayWaveLoop(i, i * 0.15f);
    }

    yield return new WaitForSeconds(0.5f);
  }

  public void PlayWaveLoop(int imageIdx,float delayTime)
  {
    StartCoroutine(PlayWave(imageIdx, delayTime));
  }

  IEnumerator PlayWave(int imageIdx,float delayTime,bool isLoop = true)
  {
    Vector2 stPos = clearImages[imageIdx].anchoredPosition;
    Vector2 edPos = stPos;
    edPos.y += 50;

    yield return new WaitForSeconds(delayTime);

    int cnt = 0;
    bool isMove = false;


    while (true)
    {
      bool isToUp = cnt % 2 == 0;
      Vector2 e = isToUp ? edPos : stPos;

      if (!isMove)
      {
        isMove = true;
        clearImages[imageIdx].DOAnchorPos(e, 0.5f).OnComplete(() => { cnt++; isMove = false; });
      }

      if (cnt >= 100)
      {
        cnt = 0;
      }

      if(cnt == 2 && !isLoop)
      {
        break;
      }

      yield return null;
    }
    
  }



  
}
