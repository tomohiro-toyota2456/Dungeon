using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParam : MonoBehaviour
{
  [SerializeField]
  EnemyParamBase param;

  int curHp;

  public int CurHp { get { return curHp; } }

  public void Init(EnemyParamBase param)
  {
    this.param = param;
    curHp = param.MaxHp;
  }

  public float Attack(float def)
  {
    return GameCommon.CalcDamage(Random.Range(param.MinAtk, param.MaxAtk), def, param.Critical);
  }

  public float Defence(float atk,int criticalPoint)
  {
    int damage = (int)GameCommon.CalcDamage(atk, param.Def, criticalPoint);
    curHp -= damage;
    return damage;
  }
}
