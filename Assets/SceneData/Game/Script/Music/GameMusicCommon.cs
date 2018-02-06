using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicCommon
{
  public static readonly string RepairSEPath = "SE/magic-cure1";
  public static readonly string PhaseSEPath = "SE/dash";

  //エフェクトから音声再生
  public static string GetSEPathFromEffectType(WeponParam.EffectType effectType)
  {
    switch(effectType)
    {
      case WeponParam.EffectType.Slashing:
        return "SE/sword-gesture1";

      case WeponParam.EffectType.Striking:
        return "SE/punch-high1";
    }

    return "";
  }
}
