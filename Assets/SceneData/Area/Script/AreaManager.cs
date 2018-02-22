using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaManager : MonoBehaviour {

	[SerializeField]
	Button[] areaButton;

	// Use this for initialization
	void Start () {
		//ダンジョンデータの取得
		DungeonDataBase.ShortData[] DungeonData = DataBaseManager.Instance.GetDataBase<DungeonDataBase>().GetShortDatas();
		Button btn;
		Text txt;
		for (int i = 0; i<DungeonData.Length; i++) {
			int index = i;
			btn = areaButton [index];
			txt = btn.gameObject.GetComponentInChildren<Text> ();
			txt.text = DungeonData [index].name;
			btn.onClick.AddListener(()=>OnClick(DungeonData[index].id));
		}

    //BGM再生
    SoundPlayer.Instance.PlayBgmCrossFade(GameMusicCommon.AreaBGMPath);

	}
	public void OnClick(int id)
	{
		DungeonManager.DungeonId = id;
		ChangeScene.Instance.LoadScene ("Game");
	}
		
}
