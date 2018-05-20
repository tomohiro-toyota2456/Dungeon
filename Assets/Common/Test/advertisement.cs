using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

/// <summary>
/// <para>注意！</para>
/// <para>ServiceのADSで</para> Testmodeになっているか確認する事
/// </summary>
public class advertisement : UnitySingleton<advertisement>{
	/// <summary>
	/// <para>各種広告ID</para>
	/// 変更しないで
	/// </summary>
	#if UNITY_ANDROID
	string gameId = "1659643";
	#elif UNITY_IOS
	string gameId = "1659644";
	#endif
	/// <summary>
	/// ムービースキップ不可用の文字列　
	/// </summary>
	string placementId = "rewardedVideo";
	// Use this for initialization
	void Start () {
		///現在の端末が広告をサポートしているか
		if(Advertisement.isSupported)
		{
			///広告の初期化
			Advertisement.Initialize (gameId,true);
		}
	}

	public void ShowAd(System.Action<ShowResult> action)
	{
		//コールバック用オプションの作成
		ShowOptions options = new ShowOptions();
    options.resultCallback = action;

		///広告再生準備の確認
		if (Advertisement.IsReady ()) {
			///広告の再生
			Advertisement.Show (placementId,options);
		}
	}

  public void ShowAD2()
  {
    Advertisement.Show();
  }

	public void HandleShowResult (ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished://広告を再生終了

			break;
		case ShowResult.Skipped://ユーザーが広告をスキップ

			break;
		case ShowResult.Failed://何らかの理由で広告が再生されなかった
			

			break;
		}
	}

}
