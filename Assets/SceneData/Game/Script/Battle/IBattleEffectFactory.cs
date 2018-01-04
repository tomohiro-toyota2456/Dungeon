using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleEffectFactory
{
  IBattleEffect PlayEffect(int type);
}
