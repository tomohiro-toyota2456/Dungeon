using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParam 
{
  EnemyParamBase param;

  int curHp;

  public int MaxHp { get { return param.MaxHp; } }
  public int CurHp { get { return curHp; } }
  public string Name { get { return param.name; } }

  public void Init(EnemyParamBase param)
  {
    this.param = param;
    curHp = param.MaxHp;
  }

  public float CalcDef()
  {
    return param.Def;
  }

  public PlayerParam.ParamData CalcAtk()
  {
    PlayerParam.ParamData param = new PlayerParam.ParamData(Random.Range(this.param.MinAtk, this.param.MaxAtk), this.param.Critical);
    return param;
  }

  public void Damage(float damage)
  {
    curHp -= (int)damage;
    curHp = curHp < 0 ? 0 : curHp;
  }
}
