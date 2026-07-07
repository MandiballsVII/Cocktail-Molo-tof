using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] private Liquid liquid;
    private List<GarnishData> garnishes = new();

    public void Pour(BottleData bottle)
    {
        liquid.Pour(bottle);
    }
    public void Clear()
    {
        liquid.Clear();
    }
    public void AddGarnish(GarnishData garnish)
    {
        garnishes.Add(garnish);
        Debug.Log("Añadido adorno: " + garnish.garnishName);
    }

    public Liquid Liquid => liquid;
}