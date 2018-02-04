using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//プレイヤーがダメージを受けたときのエフェクト
//今回は画面揺れで表現
public class DamageEffect : MonoBehaviour,IDamageEffect
{
  [SerializeField]
  RectTransform rect;

  public void PlayEffect(float time)
  {
    rect.DOShakeAnchorPos(time, 10);
  }
}
