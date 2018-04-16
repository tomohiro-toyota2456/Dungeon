using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : UnitySingleton<TutorialManager>
{
  [SerializeField]
  MessageWindow messageWindow;
  [SerializeField]
  TutorialData battleCommandTutorial;
  [SerializeField]
  Image blockImage;

  string battleCommandTutorialKey = "AWFPWAKdwaDJwdjWdjd9w0f";

  public IEnumerator StartBattleCommandTutorial()
  {
    messageWindow.Show();
    blockImage.gameObject.SetActive(true);
    string[] messages = battleCommandTutorial.Messages;
    
    for(int i = 0; i < messages.Length; i++)
    {
      messageWindow.SetMessage(messages[i]);
      yield return messageWindow.JudgeTouch(10f);
    }

    blockImage.gameObject.SetActive(false);
    messageWindow.Hide();
  }

  public bool IsFinishedBattleCommandTutorial()
  {
    int flag = PlayerPrefs.GetInt(battleCommandTutorialKey,-1);

    return flag == 1;
  }

  public void SaveFinishedCommandTutorialFlag(bool isFinished = true)
  {
    PlayerPrefs.SetInt(battleCommandTutorialKey, isFinished ? 1 : -1);
    PlayerPrefs.Save();
  }
}
