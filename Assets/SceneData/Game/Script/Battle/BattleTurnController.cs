﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTurnController : IBattleTurn
{
  PlayerParam playerParam;
  EnemyParam enemyParam;

  IBattleEffectFactory effectFactory;
  IBattleCommand battleCommand;
  IDamageEffect damageEffect;

  public PlayerParam Player { set { playerParam = value; } }
  public EnemyParam Enemy { set { enemyParam = value; } }
  public IBattleEffectFactory EffectFactory { set { effectFactory = value; } }
  public IBattleCommand BattleCommand { set { battleCommand = value; } }
  public IDamageEffect DamageEffect { set { damageEffect = value; } }
  public Button EquipmentButton { set; private get; }//装備確認ボタン

  string escapeDesc = "現在の装備のままダンジョンを\n脱出できます。逃げますか?";

  public enum ActionType
  {
    Attack,
    Repair,
    Escape,
  }

  public struct BattleLog
  {
    public string actionUserName;
    public string targetName;
    public int damage;
    public bool isCritical;
    public ActionType actionType;
  }

  BattleLog battleLog;
  public BattleLog Log { get { return battleLog; } }

  IEnumerator IBattleTurn.ExecEnemyTurn()
  {
    battleLog.actionUserName = enemyParam.Name;
    battleLog.targetName = "プレイヤー";
    battleLog.actionType = ActionType.Attack;

    PlayerParam.ParamData data = enemyParam.CalcAtk();
    //ダメージ計算
    float critical = GameCommon.CalcCriticalBonus(data.critical);
    float damage = GameCommon.CalcDamage(data.atk, playerParam.UseArmor(), critical);

    battleLog.damage = (int)damage;
    battleLog.isCritical = critical != 1;

    playerParam.Damage(damage);
    SoundPlayer.Instance.PlaySe(GameMusicCommon.GetSEPathFromEffectType(WeponParam.EffectType.Striking));//再生

    if(damage != 0)
    {
      damageEffect.PlayEffect(1.0f);
    }

    yield return null;
  }

  IEnumerator IBattleTurn.ExecPlayerTurn()
  {
    
    battleCommand.Show();

    //チュートリアル終わってなければ表示させる
    //バトルコマンドチュートリアル
    if(!TutorialManager.Instance.IsFinishedBattleCommandTutorial())
    {
      yield return TutorialManager.Instance.StartBattleCommandTutorial();

      TutorialManager.Instance.SaveFinishedCommandTutorialFlag();
    }

    //入力待ち

    //装備確認ボタン有効化
    EquipmentButton.enabled = true;

    yield return battleCommand.Command();

    //装備確認ボタン無効化
    EquipmentButton.enabled = false;


    battleCommand.Hide();

    //果たしてここでダメージ計算を行うべきなのか
    //数値管理はどこかで一元管理のほうがいい気もする。（ただこのクラスの存在意義が？）
    WeponParam.EffectType eType = WeponParam.EffectType.Slashing;
    PlayerParam.ParamData data = new PlayerParam.ParamData(0,0);

    battleLog.actionUserName = "プレイヤー";
    battleLog.targetName = enemyParam.Name;

    switch (battleCommand.ButtonType)
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
        battleLog.actionType = ActionType.Repair;
        break;

      case 3://Escape
        //
        battleLog.actionType = ActionType.Escape;
        break;
    }
    
    if(battleCommand.ButtonType <= 1)
    {
      //ダメージ計算
      float critical = GameCommon.CalcCriticalBonus(data.critical);
      float damage = GameCommon.CalcDamage(data.atk, enemyParam.CalcDef(),critical);

      battleLog.damage = (int)damage;
      battleLog.isCritical = critical != 1;
      battleLog.actionType = ActionType.Attack;

      enemyParam.Damage(damage);

      //エフェクト再生
      IBattleEffect effect = null;
      effect = effectFactory.PlayEffect(eType.GetHashCode());
      SoundPlayer.Instance.PlaySe(GameMusicCommon.GetSEPathFromEffectType(eType));//再生

      while (effect.IsAnimation)
      {
        yield return null;
      }
    }

    //回復
    if(battleCommand.ButtonType == 2)
    {
      SoundPlayer.Instance.PlaySe(GameMusicCommon.RepairSEPath);
      playerParam.UserRepair();
    }

    //逃走
    if(battleCommand.ButtonType == 3)
    {
      bool isDecision = false;
      var popup = PopupManager.Instance.CreateSimplePopup();
      popup.Init("逃げますか?", escapeDesc, () =>
      {
        ChangeScene.Instance.LoadScene("AreaMap");
        isDecision = true;
      },
      () =>
      {
        isDecision = true;
      }, "はい", "いいえ");

      PopupManager.Instance.Open(popup);

      while (!isDecision)
        yield return null;
      
    }
  }
}
