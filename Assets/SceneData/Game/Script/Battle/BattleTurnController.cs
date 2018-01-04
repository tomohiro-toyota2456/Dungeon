using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTurnController : MonoBehaviour,IBattleTurn
{
  PlayerParam playerParam;
  EnemyParam enemyParam;

  IBattleEffectFactory effectFactory;
  IBattleCommand battleCommand;

  public PlayerParam Player { set { playerParam = value; } }
  public EnemyParam Enemy { set { enemyParam = value; } }
  public IBattleEffectFactory EffectFactory { set { effectFactory = value; } }
  public IBattleCommand BattleCommand { set { battleCommand = value; } }

  IEnumerator IBattleTurn.ExecEnemyTurn()
  {
    yield return null;
  }

  IEnumerator IBattleTurn.ExecPlayerTurn()
  {
    //
    battleCommand.Show();

    //入力待ち
    yield return battleCommand.Command();

    battleCommand.Hide();

    //行動Animation
    yield return null;
  }
}
