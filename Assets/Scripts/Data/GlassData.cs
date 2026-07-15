using UnityEngine;

[CreateAssetMenu(fileName = "New Glass", menuName = "Cocktails/Glass")]
public class GlassData : ScriptableObject
{
    public string glassName;

    public Glass prefab;

    [Header("Visual")]

    [Min(0.1f)]
    public float garnishScaleMultiplier = 1f;
}