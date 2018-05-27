using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicCommon
{
  public static readonly string RepairSEPath = "SE/magic-cure1";
  public static readonly string PhaseSEPath = "SE/dash";
  public static readonly string AreaBGMPath = "BGM/Field";
  public static readonly string BossBgmPath = "BGM/Boss";
  public static readonly string GettingEquipmentSEPath = "SE/door-open1";

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

  public static string GetBgmFromId(int id)
  {
    switch(id)
    {
      case 0:
        return "BGM/stage1";

      case 1:
        return "BGM/stage2";

	  case 2:
		return "BGM/stage3";
		
	  case 3:
		return "BGM/stage4";

    }

    return null;
  }
}
