using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class WavePhase : WavePhaseBase
{
  [SerializeField]
  TextMeshProUGUI phaseText;
  [SerializeField]
  TextMeshProUGUI numberText;
  [SerializeField]
  Vector2 phaseTextBasePos;
  [SerializeField]
  Vector2 numberTextBasePos;
  [SerializeField]
  Vector2 phaseTextPos;
  [SerializeField]
  Vector2 numberTextPos;

  public override IEnumerator ExecWavePhase(int phase, int maxPhase, bool isBoss = false)
  {
    var rect = phaseText.GetComponent<RectTransform>();
    var rect2 = numberText.GetComponent<RectTransform>();

    rect.anchoredPosition = phaseTextBasePos;
    rect2.anchoredPosition = numberTextBasePos;


    phaseText.gameObject.SetActive(true);
    numberText.gameObject.SetActive(true);
    numberText.text = phase.ToString();

    bool isAnimation = true;

    rect.DOAnchorPos(phaseTextPos, 0.5f);
    rect2.DOAnchorPos(numberTextPos, 0.5f).OnComplete(()=>isAnimation = false);

    while (isAnimation)
      yield return null;

    yield return new WaitForSeconds(0.5f);

    phaseText.gameObject.SetActive(false);
    numberText.gameObject.SetActive(false);

  }

}
