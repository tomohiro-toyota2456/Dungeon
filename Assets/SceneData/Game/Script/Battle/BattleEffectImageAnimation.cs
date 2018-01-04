using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEffectImageAnimation : MonoBehaviour, IBattleEffect
{
  [SerializeField]
  Image image;
  [SerializeField]
  Sprite[] sprites;
  [SerializeField]
  float animationTime;

  bool isAnimation;
  bool IBattleEffect.IsAnimation { get { return isAnimation; } }

  void IBattleEffect.PlayAnimation()
  {
    if (isAnimation)
      return;

    StartCoroutine(PlayerAnimationCoroutine());
  }

  IEnumerator PlayerAnimationCoroutine()
  {
    isAnimation = true;
    float timer = 0;
    int idx = 0;
    float oneTime = animationTime / (float)sprites.Length;
    while(timer <= animationTime)
    {
      int t = (int)(timer / oneTime) ;
      idx = t % sprites.Length;
      timer += Time.deltaTime;
      yield return null;
    }

    isAnimation = false;
  }
}
