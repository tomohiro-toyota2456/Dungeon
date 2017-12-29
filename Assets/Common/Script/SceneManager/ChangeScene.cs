using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	private string preview;
	// Use this for initialization
	void Start () {
		
	}

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
		do {
			yield return WaitForEndOfFrame ();
		} 
		while(!Async.isDone);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
