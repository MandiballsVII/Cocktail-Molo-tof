using UnityEngine;

[CreateAssetMenu(fileName = "New Garnish", menuName = "Cocktails/Garnish")]
public class GarnishData : ScriptableObject
{
    public string garnishName;

    public Sprite sprite;

    public GameObject prefab;

    [Header("Visual")]

    [Min(0.1f)]
    public float baseScale = 1f;

    public GarnishPlacement placement;
}