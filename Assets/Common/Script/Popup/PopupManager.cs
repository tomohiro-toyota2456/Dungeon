using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : UnitySingleton<PopupManager>
{
  [SerializeField]
  Transform root;
  [SerializeField]
  Image guardImage;
  [SerializeField]
  PopupSimple popupSimple;

  public T CreatePopup<T>(T prefab) where T : PopupBase
  {
    T ins = Instantiate<T>(prefab);
    ins.transform.SetParent(root);
    ins.transform.localScale = new Vector3(0, 0, 0);
    ins.transform.localPosition = new Vector3(0, 0, 0);
    return ins;
  }

  public PopupSimple CreateSimplePopup()
  {
    PopupSimple ins = Instantiate(popupSimple);
    ins.transform.SetParent(root);
    ins.transform.localScale = new Vector3(0, 0, 0);
    ins.transform.localPosition = new Vector3(0, 0, 0);
    return ins;
  }

  public void Open(PopupBase instance)
  {
    guardImage.gameObject.SetActive(true);
    instance.AddClosedAction(() => guardImage.gameObject.SetActive(false));
    instance.Open();
  }
}
