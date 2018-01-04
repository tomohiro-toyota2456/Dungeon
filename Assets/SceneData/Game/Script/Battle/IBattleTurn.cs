using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleTurn
{
  IEnumerator ExecEnemyTurn();
  IEnumerator ExecPlayerTurn();
}
