using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    [SerializeField] private SpriteRenderer liquidRenderer;

    private Dictionary<BottleData, int> ingredients = new();

    private void Awake()
    {
        liquidRenderer.enabled = false;
    }
    public void Show()
    {
        liquidRenderer.enabled = true;
    }

    public void Hide()
    {
        liquidRenderer.enabled = false;
    }
    public void Pour(BottleData bottle)
    {
        if (ingredients.ContainsKey(bottle))
            ingredients[bottle] += bottle.units;
        else
            ingredients.Add(bottle, bottle.units);

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        liquidRenderer.color = CalculateColor();
    }

    private Color CalculateColor()
    {
        if (ingredients.Count == 0)
            return Color.clear;

        float totalUnits = 0;

        float r = 0;
        float g = 0;
        float b = 0;

        foreach (var ingredient in ingredients)
        {
            int units = ingredient.Value;

            totalUnits += units;

            r += ingredient.Key.liquidColor.r * units;
            g += ingredient.Key.liquidColor.g * units;
            b += ingredient.Key.liquidColor.b * units;
        }

        return new Color(
            r / totalUnits,
            g / totalUnits,
            b / totalUnits,
            1f
        );
    }

    public int GetUnits(BottleData bottle)
    {
        if (ingredients.TryGetValue(bottle, out int units))
            return units;

        return 0;
    }

    public int TotalUnits
    {
        get
        {
            int total = 0;

            foreach (int value in ingredients.Values)
                total += value;

            return total;
        }
    }

    public void Clear()
    {
        ingredients.Clear();
        UpdateVisual();
    } 
}