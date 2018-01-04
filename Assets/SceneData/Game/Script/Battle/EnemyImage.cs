using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyImage : MonoBehaviour
{
  [SerializeField]
  Image enemyImage;
  [SerializeField]
  Sprite enemySprite;

  public void Init(Sprite sprite)
  {
    enemyImage.sprite = sprite;
  }

  public IEnumerator FadeIn(float fadeTime)
  {
    var col = enemyImage.color;
    col.a = 0;
    enemyImage.color = col;

    bool isComp = false;
    enemyImage.DOFade(1, fadeTime).OnComplete(() => isComp = true);

    while (!isComp)
      yield return null;
  }

  public IEnumerator FadeOut(float fadeTime)
  {
    var col = enemyImage.color;
    col.a = 1;
    enemyImage.color = col;

    bool isComp = false;
    enemyImage.DOFade(0, fadeTime).OnComplete(() => isComp = true);

    while (!isComp)
      yield return null;
  }
}
