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

  WeponDataBase weponDataBase;
  ArmorDataBase armorDataBase;

  PlayerParam player;

  bool isNextBattle = false;

  public static int DungeonId { private get; set; }

	// Use this for initialization
	void Start ()
  {
    weponDataBase = DataBaseManager.Instance.GetDataBase<WeponDataBase>();
    armorDataBase = DataBaseManager.Instance.GetDataBase<ArmorDataBase>();

    player = new PlayerParam();
    player.SetMainWepon(weponDataBase.Search(0), null, null, null);
    player.SetSubWepon(weponDataBase.Search(1), null, null, null);
    player.SetArmor(armorDataBase.Search(0), null, null, null);
    player.Init();

    StartCoroutine(UpdateCoroutine());
	}
	
	// Update is called once per frame
	void Update ()
  {
    if (isNextBattle)
      StartCoroutine(UpdateCoroutine());
	}

  IEnumerator UpdateCoroutine()
  {
    isNextBattle = false;
    EnemyParam enemy = new EnemyParam();

    var p = ScriptableObject.CreateInstance<EnemyParamBase>();
    p.MaxHp = 20;
    p.MaxAtk = 20;
    p.MinAtk = 5;
    p.Def = 1;
    p.name = "ゾンビ";
    p.Critical = 5;
    enemy.Init(p);

    //フェーズ表示
    yield return wavePhase.ExecWavePhase(1, 2);
    battlePhase.Init();

    while (!isNextBattle)
    {
      yield return battlePhase.ExecBattlePhase(player, enemy);

      if (player.CurHp <= 0)
      {
        //ゲームオーバー
        isNextBattle = true;
      }
      else if(enemy.CurHp <=0)
      {
        messageWindow.SetMessage(enemy.Name + "をたおした!");
        yield return new WaitForSeconds(1.0f);
        isNextBattle = true;
      }

    }

  }
}
