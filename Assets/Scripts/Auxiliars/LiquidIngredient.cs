using UnityEngine;

[System.Serializable]
public class LiquidIngredient
{
    public BottleData bottle;

    public int units;

    public LiquidIngredient(BottleData bottle, int units)
    {
        this.bottle = bottle;
        this.units = units;
    }
}