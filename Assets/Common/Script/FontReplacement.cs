//***************************************************
//FontReplacement.cs
//Autor harada
//***************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

//***************************************************
//FontReplacement
//シーン　プレハブのText Textmeshのフォントを一括差し替え
//するクラス
//***************************************************
public class FontReplacement : EditorWindow
{
  bool isTMProSceneOnly;
  TMP_FontAsset targetTMProFont;
  TMP_FontAsset replacementTMProFont;

  [MenuItem("Replacement/Font/OpenWindow")]
  public static void OpenWindow()
  {
    GetWindow<FontReplacement>();
  }

  private void OnGUI()
  {
    using (new GUILayout.VerticalScope())
    {
      GUILayout.Label("TextMeshPro");
      GUILayout.Label("置き換え対象フォント(未指定の場合全部置き換え)");
      targetTMProFont = EditorGUILayout.ObjectField("TMP_FontAsset",targetTMProFont,typeof(TMP_FontAsset),true) as TMP_FontAsset;

      GUILayout.Label("置き換えフォント");
      replacementTMProFont = EditorGUILayout.ObjectField("TMP_FontAsset", replacementTMProFont, typeof(TMP_FontAsset), true) as TMP_FontAsset;

      isTMProSceneOnly = GUILayout.Toggle(isTMProSceneOnly,"シーン内のみ");
      if(GUILayout.Button("置き換え"))
      {

      }

    }
  }

  void ReplaceTextMeshProFont(bool isSceneOnly = false)
  {
    //全走査
    foreach(var obj in UnityEngine.Resources.FindObjectsOfTypeAll<TextMeshProUGUI>())
    {
      string path = AssetDatabase.GetAssetOrScenePath(obj);
      if(!isSceneOnly || path.Contains(".unity"))
      {
        Debug.Log(obj.font.name);
      }
    }
  }
}
