using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEffectFactory : MonoBehaviour,IBattleEffectFactory
{
  [SerializeField]
  BattleEffectImageAnimation[] animations;

  IBattleEffect IBattleEffectFactory.PlayEffect(int type)
  {
    animations[type].PlayAnimation();
    return animations[type];
  }
}
