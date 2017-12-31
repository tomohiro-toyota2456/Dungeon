using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : UnitySingleton <ChangeScene>
{
  [SerializeField]
  string initLoadSceneName;

	private string preview;

  private void Start()
  {
    LoadScene(initLoadSceneName);
  }

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
		Fade.Instance.FadeIn ();
		while (Fade.Instance.IsFade) {
			yield return null;
		}
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
		Fade.Instance.FadeOut ();
		while (Fade.Instance.IsFade) {
			yield return null;
		}
	}
		

}
