
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ScriptData", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class DialogueExample : ScriptableObject
{
    public int dialogueID = 0;
    List<string> dialogueText = new List<string>();
    short dialogueSize = 0;

}
