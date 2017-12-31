/**********************************************************
 * PopupBase.cs
 * Author harada
 * *******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

/**********************************************************
 * PopupBase
 * ポップアップOpen Closeを定義した基本クラス
 * *******************************************************/
public class PopupBase : MonoBehaviour
{
  [SerializeField]
  float animationTime = 0.5f;

  List<Action> closedAction;//閉じ終わり時の追加処理

  public virtual void Open()
  {
    transform.localScale = new Vector3(0, 0, 0);
    transform.DOScale(new Vector3(1,1,1), animationTime);
  }

  public virtual void Close()
  {
    transform.DOScale(new Vector3(0,0,0), animationTime).OnComplete(() =>
     {
       if (closedAction != null)
       {
         for(int i = 0; i < closedAction.Count; i++)
         {
           closedAction[i]();
         }
       }

       GameObject.Destroy(gameObject);
     });
  }

  public void AddClosedAction(Action act)
  {
    if (closedAction == null)
      closedAction = new List<Action>();

    closedAction.Add(act);
  }
}
