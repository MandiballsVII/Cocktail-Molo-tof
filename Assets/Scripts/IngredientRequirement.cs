using UnityEngine;

[System.Serializable]
public class IngredientRequirement
{
    public BottleData ingredient;

    [Min(1)]
    public int units;
}