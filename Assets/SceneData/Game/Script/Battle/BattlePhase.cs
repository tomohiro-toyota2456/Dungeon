﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class BattlePhase : BattlePhaseBase
{
  BattleTurnController battleTurnController = new BattleTurnController();

  bool isPlayerTurn = true;

  public override void Init()
  {
    isPlayerTurn = true;
  }

  public override IEnumerator ExecBattlePhase(PlayerParam playerParam, EnemyParam enemyParam)
  {
    messageWindow.SetMessage("");
    //HP更新
    playerHp.SetNumber(playerParam.CurHp, GameCommon.PlayerHp);
    enemyHp.SetNumber(enemyParam.CurHp, enemyParam.MaxHp);

    //コマンドUI更新
    battleCommandUI.SetItemNum(5);
    battleCommandUI.SetWeponNum(playerParam.EnableUsingCountMainWepon, playerParam.EnableUsingCountSubWepon);
    battleCommandUI.SetArmorNum(playerParam.EnableUsingCountArmor);
    battleCommandUI.SetItemNum(playerParam.EnableUsingCountRepair);

    //ターン処理
    battleTurnController.BattleCommand = battleCommandUI;
    battleTurnController.EffectFactory = battleEffectFactory;
    battleTurnController.Player = playerParam;
    battleTurnController.Enemy = enemyParam;
    battleTurnController.DamageEffect = damageEffect;
    battleTurnController.EquipmentButton = equipmentButton;

    IBattleTurn turn = battleTurnController;

    yield return isPlayerTurn ? turn.ExecPlayerTurn() : turn.ExecEnemyTurn();

    //文字
    messageWindow.SetMessage(ConvertMessageFromLog(battleTurnController.Log));
    messageWindow.Show();
    

    //入れ替え
    isPlayerTurn = !isPlayerTurn;

    if(battleTurnController.Log.actionType == BattleTurnController.ActionType.Escape)
    {
      isPlayerTurn = true;
      yield break;
    }

    //HP更新
    playerHp.SetNumber(playerParam.CurHp,GameCommon.PlayerHp);
    enemyHp.SetNumber(enemyParam.CurHp,enemyParam.MaxHp);

    yield return new WaitForSeconds(1.0f);

  }

  public string ConvertMessageFromLog(BattleTurnController.BattleLog log)
  {
    StringBuilder strBuilder = new StringBuilder();
    strBuilder.Append(log.actionUserName + "は");
    strBuilder.AppendLine();

    switch(log.actionType)
    {
      case BattleTurnController.ActionType.Attack:
        strBuilder.Append(log.targetName + "に" + log.damage +"の");
        strBuilder.AppendLine();
        string buf = log.isCritical ? "クリティカルダメージ!" : "ダメージ!";
        strBuilder.Append(buf);
        break;

      case BattleTurnController.ActionType.Repair:
        strBuilder.Append("全回復した!");
        break;

      case BattleTurnController.ActionType.Escape:

        break;
    }

    return strBuilder.ToString();
  }
}
