using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
  [SerializeField]
  WavePhaseBase wavePhase;//ウェーブ表記用
  [SerializeField]
  BattlePhaseBase battlePhase;//バトル表記用

  WeponDataBase weponDataBase;
  ArmorDataBase armorDataBase;

	// Use this for initialization
	void Start ()
  {
    weponDataBase = DataBaseManager.Instance.GetDataBase<WeponDataBase>();
    armorDataBase = DataBaseManager.Instance.GetDataBase<ArmorDataBase>();
    StartCoroutine(UpdateCoroutine());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  IEnumerator UpdateCoroutine()
  {
    PlayerParam player = new PlayerParam();
    EnemyParam enemy = new EnemyParam();

    player.SetMainWepon(weponDataBase.Search(0), null, null, null);
    player.SetSubWepon(weponDataBase.Search(1), null, null, null);
    player.SetArmor(armorDataBase.Search(0), null, null, null);
    player.Init();

    var p = ScriptableObject.CreateInstance<EnemyParamBase>();
    p.MaxHp = 200;
    p.MaxAtk = 10;
    p.MinAtk = 2;
    p.Def = 1;
    p.Critical = 5;
    enemy.Init(p);

    while(true)
    {
      yield return wavePhase.ExecWavePhase(1, 2);
      yield return battlePhase.ExecBattlePhase(player, enemy);
    }
  }
}
