using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	private string preview;
	// Use this for initialization
	void Start () {
		
	}
	/// <summary>
	/// 引数：シーンの名前
	/// </summary>
	/// <returns>The scene.</returns>
	/// <param name="loadSceneName">Load scene name.</param>
	IEnumerator LoadScene(string loadSceneName)
	{
		if(!string.IsNullOrEmpty(preview))
		{
			AsyncOperation previewAsync = SceneManager.UnloadSceneAsync (preview);

			while (!previewAsync.isDone) {
				yield return  null;
			}
		}

		AsyncOperation Async = SceneManager.LoadSceneAsync (loadSceneName, LoadSceneMode.Additive);

		while (!Async.isDone) {
			yield return  null;
		}
	}

}
