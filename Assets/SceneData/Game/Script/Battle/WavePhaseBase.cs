using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WavePhaseBase : MonoBehaviour
{
  public abstract IEnumerator ExecWavePhase(int phase, int maxPhase, bool isBoss = false);
}
