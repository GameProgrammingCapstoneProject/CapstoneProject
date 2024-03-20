
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ScriptData", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class DialogueExample : ScriptableObject
{
    public int dialogueID = 0;
    public List<string> dialogueText = new List<string>();
    public short dialogueSize = 0;
    public Sprite displayPortraitImage = null;

}
