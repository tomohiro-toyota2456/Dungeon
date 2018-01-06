﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BattlePhaseBase : MonoBehaviour
{
  [SerializeField]
  protected BattleCommandUI battleCommandUI;
  [SerializeField]
  protected BattleEffectFactory battleEffectFactory;
  [SerializeField]
  protected GaugeUI playerHp;
  [SerializeField]
  protected GaugeUI enemyHp;
  [SerializeField]
  protected TextMeshProUGUI enemyNameText;
  [SerializeField]
  protected TextMeshProUGUI playerNameText;

  public abstract IEnumerator ExecBattlePhase(PlayerParam playerParam, EnemyParam enemyParam);
}
