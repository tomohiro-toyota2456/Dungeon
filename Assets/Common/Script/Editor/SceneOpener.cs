using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneOpener : EditorWindow
{
  Vector2 scrollPos;

  [MenuItem("SceneOpener/OpenWindow")]
  static void Open()
  {
    GetWindow<SceneOpener>();
  }

  private void OnGUI()
  {
    GUILayout.Label("BuildSettingに登録されているシーンを開けます");
    var scenes = EditorBuildSettings.scenes;

    using (var scroll = new EditorGUILayout.ScrollViewScope(scrollPos))
    {
      for (int i = 0; i < scenes.Length; i++)
      {
        if (GUILayout.Button(scenes[i].path))
        {

          EditorSceneManager.OpenScene(scenes[i].path);
        }
      }

      scrollPos = scroll.scrollPosition;
    }
  }
}
