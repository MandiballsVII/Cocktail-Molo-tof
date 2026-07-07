using UnityEngine;

[CreateAssetMenu(fileName = "New Glass", menuName = "Cocktails/Glass")]
public class GlassData : ScriptableObject
{
    public string glassName;
    public Sprite sprite;
    public int capacity;
}