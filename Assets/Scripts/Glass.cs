using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] private Liquid liquid;
    private List<GarnishData> garnishes = new();
    [SerializeField] private Transform garnishContainer;

    public void Pour(BottleData bottle)
    {
        liquid.Show();
        liquid.Pour(bottle);
    }
    public void Clear()
    {
        liquid.Hide();
        liquid.Clear();
    }
    public void AddGarnish(GarnishData garnish)
    {
        garnishes.Add(garnish);
        Debug.Log("Añadido adorno: " + garnish.garnishName);
        GameObject garnishObject = Instantiate(garnish.prefab, garnishContainer);
        garnishObject.transform.localPosition =
            new Vector3(
                Random.Range(-0.25f, 0.25f),
                Random.Range(-0.15f, 0.15f),
                0);
        garnishObject.transform.localRotation =
            Quaternion.Euler(
                0,
                0,
                Random.Range(-30, 30));
    }

    public Liquid Liquid => liquid;
}