using UnityEngine;

[CreateAssetMenu(fileName = "New Garnish", menuName = "Cocktails/Garnish")]
public class GarnishData : ScriptableObject
{
    public string garnishName;

    public Sprite sprite;

    public GameObject prefab;
}