using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : UnitySingleton <Fade> {

	[SerializeField]
	Image fadeImage;

	/// <summary>
	/// フェードフラグ
	/// </summary>
	private bool isFade = false;

	/// <summary>
	/// フェードにかかる時間
	/// </summary>
	private float fadeTime = 1.0f;

	/// <summary>
	/// フェードフラグ引き渡し
	/// </summary>
	/// <value><c>true</c> if this instance is fade; otherwise, <c>false</c>.</value>
	public bool IsFade { get { return isFade; } }

	// Use this for initialization
	void Start () {
		DOTween.Init (true, true);
	}
	
	public void FadeIn()
	{
		isFade = true;
    fadeImage.gameObject.SetActive(true);
		fadeImage.DOFade (1.0f, fadeTime).OnComplete(()=>{isFade=false;});
	}
	public void FadeOut()
	{
		isFade = true;
		fadeImage.DOFade (0f, fadeTime).OnComplete(()=>
    {
      isFade =false;
      fadeImage.gameObject.SetActive(false);
    });
	}
}
