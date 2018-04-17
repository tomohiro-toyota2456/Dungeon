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
  TutorialData dropTutorial;
  [SerializeField]
  Image blockImage;

  string battleCommandTutorialKey = "AWFPWAKdwaDJwdjWdjd9w0f";
  string dropTutorialKey = "AFPOFLW224042412w0eonWE";

  public IEnumerator StartBattleCommandTutorial()
  {
    string[] messages = battleCommandTutorial.Messages;
    yield return StartTutorial(messages);

  }

  public IEnumerator StartDropTutorial()
  {
    string[] messages = dropTutorial.Messages;
    yield return StartTutorial(messages);
  }

  IEnumerator StartTutorial(string[] messages)
  {
    messageWindow.Show();
    blockImage.gameObject.SetActive(true);
    for (int i = 0; i < messages.Length; i++)
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

  public bool IsFinishedDropTutorial()
  {
    int flag = PlayerPrefs.GetInt(dropTutorialKey, -1);

    return flag == 1;
  }

  public void SaveFinishedCommandTutorialFlag(bool isFinished = true)
  {
    PlayerPrefs.SetInt(battleCommandTutorialKey, isFinished ? 1 : -1);
    PlayerPrefs.Save();
  }

  public void SaveFinishedDropTutorialFlag(bool isFinished = true)
  {
    PlayerPrefs.SetInt(dropTutorialKey, isFinished ? 1 : -1);
    PlayerPrefs.Save();
  }
}
