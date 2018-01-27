using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
  [SerializeField]
  WavePhaseBase wavePhase;//ウェーブ表記用
  [SerializeField]
  BattlePhaseBase battlePhase;//バトル表記用
  [SerializeField]
  MessageWindow messageWindow;
  [SerializeField]
  ClearEffectBase clearEffect;
  [SerializeField]
  EquipmentChangePopup equipmentChangePopup;//装備変更確認ポップアップ

  WeponDataBase weponDataBase;
  ArmorDataBase armorDataBase;
  EnemyDataBase enemyDataBase;
  EnemyPopTableDataBase popDataBase;
  OptionDataBase optionDataBase;
  DropItemTableDataBase dropItemTableDataBase;
  
  PlayerParam player;

  bool isNextBattle = false;
  PopupManager popupmanager;

  public static int DungeonId { private get; set; }

  DungeonData dungeonData;
  int phase = 1;
  int maxPhase = 0;

  PlayerData playerData = new PlayerData();

	// Use this for initialization
	void Start ()
  {
    weponDataBase = DataBaseManager.Instance.GetDataBase<WeponDataBase>();
    armorDataBase = DataBaseManager.Instance.GetDataBase<ArmorDataBase>();
    enemyDataBase = DataBaseManager.Instance.GetDataBase<EnemyDataBase>();
    popDataBase = DataBaseManager.Instance.GetDataBase<EnemyPopTableDataBase>();
    optionDataBase = DataBaseManager.Instance.GetDataBase<OptionDataBase>();
    dropItemTableDataBase = DataBaseManager.Instance.GetDataBase<DropItemTableDataBase>();

    playerData.LoadEquipmentData();

    player = new PlayerParam();
    player.SetMainWepon(weponDataBase.Search(playerData.MainWepon.id,WeponParam.WeponType.Main),playerData.MainWepon.options);
    player.SetSubWepon(weponDataBase.Search(playerData.SubWepon.id,WeponParam.WeponType.Sub), playerData.SubWepon.options);
    player.SetArmor(armorDataBase.Search(playerData.Armor.id),playerData.Armor.options);
    player.Init();

    dungeonData = DataBaseManager.Instance.GetDataBase<DungeonDataBase>().Search(0);
    phase = 1;
    maxPhase = dungeonData.AppearanceTableIds.Length;

    popupmanager = PopupManager.Instance;

    StartCoroutine(UpdateCoroutine());
	}
	
	// Update is called once per frame
	void Update ()
  {
    if (isNextBattle)
      StartCoroutine(UpdateCoroutine());
	}

  string GetAppearanceStr(EnemyParamBase.EnemyType enemyType)
  {
    switch(enemyType)
    {
      case EnemyParamBase.EnemyType.Monster:

        return "があらわれた!";

      case EnemyParamBase.EnemyType.Object:

        return "をみつけた!";
    }

    return "";
  }

  string GetEndStr(EnemyParamBase.EnemyType enemyType)
  {
    switch (enemyType)
    {
      case EnemyParamBase.EnemyType.Monster:

        return "をたおした!";

      case EnemyParamBase.EnemyType.Object:

        return "をひらいた!";
    }

    return "";
  }

  IEnumerator UpdateCoroutine()
  {
    isNextBattle = false;
    EnemyParam enemy = new EnemyParam();

    //出現エネミー検索
    int tableId = dungeonData.AppearanceTableIds[phase - 1];
    int enemyId = popDataBase.Search(tableId).GetRandomId();
    EnemyParamBase enemyParamBase = enemyDataBase.Search(enemyId);
    enemy.Init(enemyParamBase);

    //フェーズ表示
    yield return wavePhase.ExecWavePhase(phase, maxPhase);
    battlePhase.Init();

    messageWindow.SetMessage(enemy.Name + GetAppearanceStr(enemy.Type));
    yield return new WaitForSeconds(1f);

    while (!isNextBattle)
    {
      if (player.CurHp <= 0)
      {
        //ゲームオーバー
        isNextBattle = true;
        break;
      }
      else if (enemy.CurHp <= 0)
      {
        messageWindow.SetMessage(enemy.Name + GetEndStr(enemy.Type));
        yield return new WaitForSeconds(1.0f);
        phase++;
        break;
      }

      if (player.EnableUsingCountMainWepon <= 0)
      {
        player.SetMainWepon(weponDataBase.GetDefaultWepon(WeponParam.WeponType.Main), null, null, null);
        playerData.SetMainWepon(-1, new EquipmentOptionBase[3]);
        playerData.SaveEquipmentData();
      }

      if (player.EnableUsingCountSubWepon <= 0)
      {
        player.SetSubWepon(weponDataBase.GetDefaultWepon(WeponParam.WeponType.Sub), null, null, null);
        playerData.SetSubWepon(-1, new EquipmentOptionBase[3]);
        playerData.SaveEquipmentData();
      }

      if (player.EnableUsingCountArmor <= 0)
      {
        player.SetArmor(armorDataBase.GetDefaultArmor(), null, null, null);
        playerData.SetArmor(-1, new EquipmentOptionBase[3]);
        playerData.SaveEquipmentData();
      }

      yield return battlePhase.ExecBattlePhase(player, enemy);
    }

    //ドロップ抽選
    if (Random.Range(0, 100) < enemy.DropPer)
    {
      var popup = popupmanager.CreatePopup<EquipmentChangePopup>(equipmentChangePopup);
      int dropTableId = enemy.DropTableId;
      var table = dropItemTableDataBase.Search(dropTableId);
      var data = table.GetRandom();

      if (data.dropType == DropTable.DropType.Wepon)
      {
        var wepon = weponDataBase.Search(data.id);

        EquipmentOptionBase[] options = optionDataBase.CalcWeponOption(wepon.MinAtk, 0, wepon.Durability
          , dungeonData.MinAtkOp, dungeonData.MaxAtkOp, dungeonData.MinCtOp, dungeonData.MaxCtOp,dungeonData.MinDura,dungeonData.MaxDura);

        if (wepon.Type == WeponParam.WeponType.Main)
        {
          PlayerEquipmentWepon eWepon = new PlayerEquipmentWepon(WeponParam.WeponType.Main);
          eWepon.Equip(wepon, options[0], options[1], options[2]);

          popup.Init(EquipmentChangePopup.EquipmentType.Wepon, player.GetEquipmentImageIds()[0], player.CalcMainWeponMaxAtk(), player.CalcMainWeponCritical(), player.EnableUsingCountMainWepon,
                     wepon.ImageId, eWepon.CalcMaxAtk(), eWepon.CalcCritical(), eWepon.CalcDurability());

          popupmanager.Open(popup);
          yield return popup.WaitDecision();

          //装備更新
          if (!popup.IsSelectedNowEquipment)
          {
            player.SetMainWepon(wepon, options);
            playerData.SetMainWepon(data.id, options);
            playerData.SaveEquipmentData();
          }
        }
        else
        {
          PlayerEquipmentWepon eWepon = new PlayerEquipmentWepon(WeponParam.WeponType.Sub);
          eWepon.Equip(wepon, options[0], options[1], options[2]);

          popup.Init(EquipmentChangePopup.EquipmentType.Wepon, player.GetEquipmentImageIds()[0], player.CalcSubWeponMaxAtk(), player.CalcSubWeponCritical(), player.EnableUsingCountSubWepon,
                     wepon.ImageId, eWepon.CalcMaxAtk(), eWepon.CalcCritical(), eWepon.CalcDurability());

          popupmanager.Open(popup);
          yield return popup.WaitDecision();

          //装備更新
          if (!popup.IsSelectedNowEquipment)
          {
            player.SetSubWepon(wepon, options);
            playerData.SetSubWepon(data.id, options);
            playerData.SaveEquipmentData();
          }
        }
      }
      else if(data.dropType == DropTable.DropType.Armor)
      {
        var armor = armorDataBase.Search(data.id);
        EquipmentOptionBase[] options = optionDataBase.CalcArmorOption(armor.Def, armor.Durability, dungeonData.MinDefOp, dungeonData.MaxDefOp, dungeonData.MinDura, dungeonData.MaxDura);

        PlayerEquipmentArmor eArmor = new PlayerEquipmentArmor();
        eArmor.Equip(armor, options[0], options[1], options[2]);

        popup.Init(EquipmentChangePopup.EquipmentType.Armor, player.GetEquipmentImageIds()[2], player.CalcArmorDef(), 0, player.EnableUsingCountArmor,
          eArmor.ImageId, eArmor.CalcDef(), 0, eArmor.CalcDurability());

        popupmanager.Open(popup);
        yield return popup.WaitDecision();

        //装備更新
        if(!popup.IsSelectedNowEquipment)
        {
          player.SetArmor(armor, options);
          playerData.SetArmor(data.id, options);
          playerData.SaveEquipmentData();
        }
      }

      isNextBattle = true;

      if (phase > maxPhase)
      {
        isNextBattle = false;
        yield return clearEffect.PlayEffect();

        yield return new WaitForSeconds(10.0f);

        //戻る
        ChangeScene.Instance.LoadScene("AreaMap");
      }


    }

  }
}
