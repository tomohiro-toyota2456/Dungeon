using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCommon
{
  public static readonly int MaxOptionNum = 3;
  public static readonly int PlayerHp = 100;
  public static readonly int baseCritical = 5;
  public static float CalcRandomDamage()
  {
    return Random.Range(0, 5.0f);
  }

  public static float CalcCriticalBonus(int addCriticalPoint)
  {
    float t = Random.Range(0,100) < (baseCritical + addCriticalPoint) ? 2.0f : 1.0f;
    return t;
  }

  public static float CalcDamage(float atk,float def,float critical)
  {
    float t = critical;
    return Mathf.Max(atk * t - def + GameCommon.CalcRandomDamage());
  }
}
