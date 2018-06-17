using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using TMPro;

public class DungeonManager : MonoBehaviour
{
  [SerializeField]
  WavePhaseBase wavePhase;//ウェーブ表記用
  [SerializeField]
  BattlePhaseBase battlePhase;//バトル表記用
  [SerializeField]
  MessageWindow messageWindow;
  [SerializeField]
  EnemyImage enemyImage;
  [SerializeField]
  ClearEffectBase clearEffect;
  [SerializeField]
  EquipmentChangePopup equipmentChangePopup;//装備変更確認ポップアップ
  [SerializeField]
  Image bg;
  [SerializeField]
  TextMeshProUGUI phaseNumText;
  [SerializeField]
  EquipmentPopup equipmentPopup;

  WeponDataBase weponDataBase;
  ArmorDataBase armorDataBase;
  EnemyDataBase enemyDataBase;
  EnemyPopTableDataBase popDataBase;
  OptionDataBase optionDataBase;
  DropItemTableDataBase dropItemTableDataBase;
  
  PlayerParam player;
  EnemyParam enemy = new EnemyParam();

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

    dungeonData = DataBaseManager.Instance.GetDataBase<DungeonDataBase>().Search(DungeonId);
    phase = 1;
    maxPhase = dungeonData.AppearanceTableIds.Length;

    //BGM再生
    SoundPlayer.Instance.PlayBgmCrossFade(GameMusicCommon.GetBgmFromId(dungeonData.MusicId));

    //BGロード
    bg.sprite = ResourceLoader.LoadDungeonBG(DungeonId);

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
  
    //出現エネミー検索
    int tableId = dungeonData.AppearanceTableIds[phase - 1];
    int enemyId = popDataBase.Search(tableId).GetRandomId();
    EnemyParamBase enemyParamBase = enemyDataBase.Search(enemyId);
    enemy.Init(enemyParamBase);
    Sprite enemysp = ResourceLoader.LoadEnemySprite(enemyParamBase.ImageId);
    enemyImage.Init(enemysp);

    //フェーズ表示
    SoundPlayer.Instance.PlaySe(GameMusicCommon.PhaseSEPath);

    //ラストフェーズの場合ボスBGMに変更
    if (phase == maxPhase)
      SoundPlayer.Instance.PlayBgmCrossFade(GameMusicCommon.BossBgmPath);


    //表示フェーズ更新
    phaseNumText.text = phase.ToString() + "/" + maxPhase.ToString();

    yield return wavePhase.ExecWavePhase(phase, maxPhase);
    battlePhase.Init();

    messageWindow.SetMessage(enemy.Name + GetAppearanceStr(enemy.Type));
    yield return enemyImage.FadeIn(0.3f);
    yield return new WaitForSeconds(0.2f);

    while (!isNextBattle)
    {
      if (player.CurHp <= 0)
      {
        messageWindow.SetMessage("プレイヤーはしんでしまった");
        yield return new WaitForSeconds(1.5f);

        yield return Ad();

        //復活しなかったということなので終わる
        if(player.CurHp <=0)
        {
          ChangeScene.Instance.LoadScene("AreaMap");
          isNextBattle = false;

          //初期装備でセーブ 死亡時全損
          playerData.SetMainWepon(-1, new EquipmentOptionBase[3]);
          playerData.SetSubWepon(-1, new EquipmentOptionBase[3]);
          playerData.SetArmor(-1, new EquipmentOptionBase[3]);
          playerData.SaveEquipmentData();

          yield break;
        }
      }
      else if (enemy.CurHp <= 0)
      {

        messageWindow.SetMessage(enemy.Name + GetEndStr(enemy.Type));
        yield return enemyImage.FadeOut(0.3f);
        yield return new WaitForSeconds(0.2f);

        phase++;
        break;
      }

      if (player.EnableUsingCountMainWepon <= 0)
      {
        messageWindow.SetMessage(player.MainWeponName + "はこわれた!");
        player.SetMainWepon(weponDataBase.GetDefaultWepon(WeponParam.WeponType.Main), null, null, null);
        playerData.SetMainWepon(-1, new EquipmentOptionBase[3]);
        playerData.SaveEquipmentData();
        yield return new WaitForSeconds(0.3f);
      }

      if (player.EnableUsingCountSubWepon <= 0)
      {
        messageWindow.SetMessage(player.SubWeponName + "はこわれた!");
        player.SetSubWepon(weponDataBase.GetDefaultWepon(WeponParam.WeponType.Sub), null, null, null);
        playerData.SetSubWepon(-1, new EquipmentOptionBase[3]);
        playerData.SaveEquipmentData();
        yield return new WaitForSeconds(0.3f);
      }

      if (player.EnableUsingCountArmor <= 0)
      {
        messageWindow.SetMessage(player.ArmorName + "はこわれた!");
        player.SetArmor(armorDataBase.GetDefaultArmor(), null, null, null);
        playerData.SetArmor(-1, new EquipmentOptionBase[3]);
        playerData.SaveEquipmentData();
        yield return new WaitForSeconds(0.3f);
      }

      yield return battlePhase.ExecBattlePhase(player, enemy);
    }

    //ドロップ抽選
    if (Random.Range(0, 100) < enemy.DropPer)
    {
      //チュートリアル判定
      if(!TutorialManager.Instance.IsFinishedDropTutorial())
      {
        yield return TutorialManager.Instance.StartDropTutorial();

        TutorialManager.Instance.SaveFinishedDropTutorialFlag();
      }

      //SE再生
      SoundPlayer.Instance.PlaySe(GameMusicCommon.GettingEquipmentSEPath);

      var popup = popupmanager.CreatePopup<EquipmentChangePopup>(equipmentChangePopup);
      int dropTableId = enemy.DropTableId;
      var table = dropItemTableDataBase.Search(dropTableId);
      var data = table.GetRandom();

      if (data.dropType == DropTable.DropType.Wepon)
      {
        var wepon = weponDataBase.Search(data.id);

        EquipmentOptionBase[] options = optionDataBase.CalcWeponOption(wepon.MinAtk, 0, wepon.Durability
          , dungeonData.MinAtkOp, dungeonData.MaxAtkOp, dungeonData.MinCtOp, dungeonData.MaxCtOp,dungeonData.MinDuraOp,dungeonData.MaxDuraOp);

        if (wepon.Type == WeponParam.WeponType.Main)
        {
          PlayerEquipmentWepon eWepon = new PlayerEquipmentWepon(WeponParam.WeponType.Main);
          eWepon.Equip(wepon, options[0], options[1], options[2]);

          popup.Init(EquipmentChangePopup.EquipmentType.Wepon, player.GetEquipmentImageIds()[0],player.MainWeponName, player.CalcMainWeponMaxAtk(), player.CalcMainWeponCritical(), player.EnableUsingCountMainWepon,
                     wepon.ImageId,wepon.Name,eWepon.CalcMaxAtk(), eWepon.CalcCritical(), eWepon.CalcDurability());

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

          popup.Init(EquipmentChangePopup.EquipmentType.Wepon, player.GetEquipmentImageIds()[1],player.SubWeponName, player.CalcSubWeponMaxAtk(), player.CalcSubWeponCritical(), player.EnableUsingCountSubWepon,
                     wepon.ImageId,wepon.Name, eWepon.CalcMaxAtk(), eWepon.CalcCritical(), eWepon.CalcDurability());

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
        EquipmentOptionBase[] options = optionDataBase.CalcArmorOption(armor.Def, armor.Durability, dungeonData.MinDefOp, dungeonData.MaxDefOp, dungeonData.MinDuraOp, dungeonData.MaxDuraOp);

        PlayerEquipmentArmor eArmor = new PlayerEquipmentArmor();
        eArmor.Equip(armor, options[0], options[1], options[2]);

        popup.Init(EquipmentChangePopup.EquipmentType.Armor, player.GetEquipmentImageIds()[2],player.ArmorName, player.CalcArmorDef(), 0, player.EnableUsingCountArmor,
          eArmor.ImageId,armor.Name,eArmor.CalcDef(), 0, eArmor.CalcDurability());

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
    }

    isNextBattle = true;

    if (phase > maxPhase)
    {
      isNextBattle = false;
      yield return clearEffect.PlayEffect();

      yield return new WaitForSeconds(2.0f);

      //戻る
      ChangeScene.Instance.LoadScene("AreaMap");
    }

  }

  public void OpenEquipmentPopup()
  {
    var pp = popupmanager.CreatePopup<EquipmentPopup>(equipmentPopup);
    pp.Init(player);
    popupmanager.Open(pp);
  }

  IEnumerator Ad()
  {
    bool isWait = true;
    if(Advertisement.isSupported)
    {
      var simplePopup = popupmanager.CreateSimplePopup();
      simplePopup.Init("広告を見ると復活できます", "復活時にHPと装備耐久値が全回復します",
        () =>
        {
          advertisement.Instance.ShowAd((result) =>
          {
            //全回復処理
            player.Init();
            isWait = false;
          });
        },
        () =>
        {
          isWait = false;
        }, "はい", "いいえ");

      popupmanager.Open(simplePopup);

      while (isWait)
        yield return null;
    }
  }
}
