using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCommandUI : MonoBehaviour, IBattleCommand
{
  [SerializeField]
  BattleCommandButton mainWeponButton;
  [SerializeField]
  BattleCommandButton subWeponButton;
  [SerializeField]
  BattleCommandButton itemButton;
  [SerializeField]
  BattleCommandButton escapeButton;
  [SerializeField]
  GameObject root;

  int buttonType = -1;
  int IBattleCommand.ButtonType { get { return buttonType; } }

  bool isClickedButton = false;

  private void Start()
  {
    mainWeponButton.onClick.RemoveAllListeners();
    subWeponButton.onClick.RemoveAllListeners();
    itemButton.onClick.RemoveAllListeners();
    escapeButton.onClick.RemoveAllListeners();

    mainWeponButton.onClick.AddListener(OnClickMainWeponButton);
    subWeponButton.onClick.AddListener(OnClickSubWeponButton);
    itemButton.onClick.AddListener(OnClickItemButton);
    escapeButton.onClick.AddListener(OnClickEscapeButton);
  }

  public void SetWeponNum(int mainNum, int subNum)
  {
    mainWeponButton.SetNumber(mainNum);
    subWeponButton.SetNumber(subNum);
  }

  public void SetItemNum(int num)
  {
    itemButton.SetNumber(num);
  }

  public void OnClickMainWeponButton()
  {
    buttonType = 0;
    isClickedButton = true;
  }

  public void OnClickSubWeponButton()
  {
    buttonType = 1;
    isClickedButton = true;
  }

  public void OnClickItemButton()
  {
    buttonType = 2;
    isClickedButton = true;
  }

  public void OnClickEscapeButton()
  {
    buttonType = 3;
    isClickedButton = true;
  }

  IEnumerator IBattleCommand.Command()
  {
    while (!isClickedButton)
      yield return null;

    isClickedButton = false;
  }

  void IBattleCommand.Show()
  {
    root.SetActive(true);
  }

  void IBattleCommand.Hide()
  {
    root.SetActive(false);
  }
}
