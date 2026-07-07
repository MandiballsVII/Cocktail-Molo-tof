using UnityEngine;

[CreateAssetMenu(menuName = "Cocktails/Bottle")]
public class BottleData : ScriptableObject
{
    public string bottleName;

    public Color liquidColor;

    public float amount = 1f;
}