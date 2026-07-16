using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "New Dialogue",
    menuName = "Cocktails/Dialogue")]
public class DialogueData : ScriptableObject
{
    public string dialogueName;

    public DialogueType type;

    public List<DialogueLine> lines = new();
}