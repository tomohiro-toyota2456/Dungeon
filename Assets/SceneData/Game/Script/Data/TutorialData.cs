using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="TutorialData",menuName ="TutorialData",order = 100)]
public class TutorialData : ScriptableObject
{
  [SerializeField]
  string[] messages;

  public string[] Messages { get { return messages; } set { messages = value; } }
}
