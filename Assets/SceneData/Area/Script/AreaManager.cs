using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AreaManager : MonoBehaviour {

	[SerializeField]
	Button[] areaButton;
  [SerializeField]
  Image[] clearImages;

	// Use this for initialization
	void Start ()
  {
		//ダンジョンデータの取得
		DungeonDataBase.ShortData[] DungeonData = DataBaseManager.Instance.GetDataBase<DungeonDataBase>().GetShortDatas();
		Button btn;
		TextMeshProUGUI txt;
		for (int i = 0; i<DungeonData.Length; i++)
    {
			int index = i;
			btn = areaButton [index];
			txt = btn.gameObject.GetComponentInChildren<TextMeshProUGUI> ();
			txt.text = DungeonData [index].name;
			btn.onClick.AddListener(()=>OnClick(DungeonData[index].id));
		}

    //クリア状況取得
    DungeonClearData.ClearData clearData = DungeonClearData.LoadClearData(DungeonData.Length);

    //クリアの場合クリアアイコン表示
    for(int i = 0; i < DungeonData.Length; i++)
    {
      if(clearData.isClears[i])
      {
        clearImages[i].gameObject.SetActive(true);
      }
    }

    //送っておく
    DungeonManager.ClearData = clearData;

    //BGM再生
    SoundPlayer.Instance.PlayBgmCrossFade(GameMusicCommon.AreaBGMPath);

	}
	public void OnClick(int id)
	{
		DungeonManager.DungeonId = id;
		ChangeScene.Instance.LoadScene ("Game");
	}

  public void Test()
  {
    advertisement.Instance.ShowAD2();
  }

}
