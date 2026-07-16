using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "New Dialogue Collection",
    menuName = "Cocktails/Dialogue Collection")]
public class DialogueCollection : ScriptableObject
{
    public string collectionName;

    [TextArea(2, 5)]
    public List<string> lines = new();

    public string GetRandomLine(params object[] args)
    {
        string line = lines[Random.Range(0, lines.Count)];

        return string.Format(line, args);
    }
}