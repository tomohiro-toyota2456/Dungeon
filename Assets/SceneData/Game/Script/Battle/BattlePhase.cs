using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePhase : BattlePhaseBase
{
  BattleTurnController battleTurnController = new BattleTurnController();

  bool isPlayerTurn = true;

  public override IEnumerator ExecBattlePhase(PlayerParam playerParam, EnemyParam enemyParam)
  {
    //HP更新
    playerHp.SetNumber(playerParam.CurHp, GameCommon.PlayerHp);
    enemyHp.SetNumber(enemyParam.CurHp, enemyParam.MaxHp);

    //コマンドUI更新
    battleCommandUI.SetItemNum(5);
    battleCommandUI.SetWeponNum(playerParam.EnableUsingCountMainWepon, playerParam.EnableUsingCountSubWepon);

    //ターン処理
    battleTurnController.BattleCommand = battleCommandUI;
    battleTurnController.EffectFactory = battleEffectFactory;
    battleTurnController.Player = playerParam;
    battleTurnController.Enemy = enemyParam;

    IBattleTurn turn = battleTurnController;

    yield return isPlayerTurn ? turn.ExecPlayerTurn() : turn.ExecEnemyTurn();

    //入れ替え
    isPlayerTurn = !isPlayerTurn;

    //HP更新
    playerHp.SetNumber(playerParam.CurHp,GameCommon.PlayerHp);
    enemyHp.SetNumber(enemyParam.CurHp,enemyParam.MaxHp);

  }
}
