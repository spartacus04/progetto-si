using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogueScriptableObject", order = 1)]
public class ScriptableDialogue : ScriptableObject
{
    public string Nome;
    public List<string> Message;

}
