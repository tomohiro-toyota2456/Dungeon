using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : UnitySingleton <ChangeScene> {

	private string preview;


	public void LoadScene(string loadSceneName)
	{
		StartCoroutine (_LoadScene (loadSceneName));
	}

	/// <summary>
	/// 引数：シーンの名前
	/// </summary>
	/// <returns>The scene.</returns>
	/// <param name="loadSceneName">Load scene name.</param>
	IEnumerator _LoadScene(string loadSceneName)
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

		preview = loadSceneName;

	}
		

}
