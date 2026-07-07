using UnityEngine;

[CreateAssetMenu(menuName = "Cocktails/Bottle")]
public class BottleData : ScriptableObject
{
    public string bottleName;

    public Color liquidColor;

    [Min(1)]
    public int units = 1;
}