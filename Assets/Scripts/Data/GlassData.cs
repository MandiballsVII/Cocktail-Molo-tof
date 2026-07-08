using UnityEngine;

[CreateAssetMenu(fileName = "New Glass", menuName = "Cocktails/Glass")]
public class GlassData : ScriptableObject
{
    public string glassName;
    public Sprite front;
    public Sprite back;
    public Sprite drink;
    public GlassType type;
}