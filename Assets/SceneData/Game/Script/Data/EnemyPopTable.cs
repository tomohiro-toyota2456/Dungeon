using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopTable : ScriptableObject
{
  [SerializeField]
  int id;
  [SerializeField]
  int[] enemyIds;

  public int Id { get { return id; } set { id = value; } }
  public int[] EnemyIds { get { return enemyIds; } set { enemyIds = value; } }

  public int GetRandomId()
  {
    int idx = Random.Range(0, enemyIds.Length);
    return enemyIds[idx];
  }
}
