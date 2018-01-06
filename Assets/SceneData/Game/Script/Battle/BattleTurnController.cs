using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTurnController : IBattleTurn
{
  PlayerParam playerParam;
  EnemyParam enemyParam;

  IBattleEffectFactory effectFactory;
  IBattleCommand battleCommand;

  public PlayerParam Player { set { playerParam = value; } }
  public EnemyParam Enemy { set { enemyParam = value; } }
  public IBattleEffectFactory EffectFactory { set { effectFactory = value; } }
  public IBattleCommand BattleCommand { set { battleCommand = value; } }

  public struct BattleLog
  {
    public int damage;
    public bool isCritical;
  }

  BattleLog battleLog;
  public BattleLog Log { get { return battleLog; } }

  IEnumerator IBattleTurn.ExecEnemyTurn()
  {
    PlayerParam.ParamData data = enemyParam.CalcAtk();
    //ダメージ計算
    float critical = GameCommon.CalcCriticalBonus(data.critical);
    float damage = GameCommon.CalcDamage(data.atk, playerParam.UseArmor(), critical);

    battleLog.damage = (int)damage;
    battleLog.isCritical = critical != 1;

    playerParam.Damage(damage);

    yield return null;
  }

  IEnumerator IBattleTurn.ExecPlayerTurn()
  {
    //
    battleCommand.Show();

    //入力待ち
    yield return battleCommand.Command();

    battleCommand.Hide();

    //果たしてここでダメージ計算を行うべきなのか
    //数値管理はどこかで一元管理のほうがいい気もする。（ただこのクラスの存在意義が？）
    WeponParam.EffectType eType = WeponParam.EffectType.Slashing;
    PlayerParam.ParamData data = new PlayerParam.ParamData(0,0);

    switch(battleCommand.ButtonType)
    {
      case 0://Main
        eType = playerParam.MainWeponEffectType;
        data = playerParam.UseMainWepon();
        break;

      case 1://Sub
        eType = playerParam.SubWeponEffectType;
        data = playerParam.UseSubWepon();
        break;

      case 2://Item
        //
        break;

      case 3://Escape
        //
        break;
    }

    
    if(battleCommand.ButtonType <= 1)
    {
      //ダメージ計算
      float critical = GameCommon.CalcCriticalBonus(data.critical);
      float damage = GameCommon.CalcDamage(data.atk, enemyParam.CalcDef(),critical);

      battleLog.damage = (int)damage;
      battleLog.isCritical = critical != 1;

      enemyParam.Damage(damage);

      //エフェクト再生
      IBattleEffect effect = null;
      effect = effectFactory.PlayEffect(eType.GetHashCode());

      while(effect.IsAnimation)
      {
        yield return null;
      }
    }
  }
}
