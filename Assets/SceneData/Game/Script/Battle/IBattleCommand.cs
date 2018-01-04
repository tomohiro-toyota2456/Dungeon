using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleCommand
{
  void Show();
  void Hide();
  IEnumerator Command();//コマンド待ち
  int ButtonType { get; }//取得
}
