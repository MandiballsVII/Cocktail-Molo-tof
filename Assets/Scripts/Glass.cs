using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] private Liquid liquid;
    private List<GarnishData> garnishes = new();
    public IReadOnlyList<GarnishData> Garnishes => garnishes;

    [SerializeField] private Transform garnishContainer;

    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;
    [SerializeField] private Collider2D stirZone;

    public Transform LeftLimit => leftLimit;
    public Transform RightLimit => rightLimit;
    public Collider2D StirZone => stirZone;

    [SerializeField] private StirSpoon spoonPrefab;

    public StirSpoon SpoonPrefab => spoonPrefab;

    [SerializeField] private Transform spoonSpawnPoint;

    public Transform SpoonSpawnPoint => spoonSpawnPoint;

    [SerializeField]
    private GlassData data;

    public GlassData Data => data;

    public void Pour(BottleData bottle)
    {
        liquid.Show();
        liquid.Pour(bottle);
    }
    public void Clear()
    {
        liquid.Hide();
        liquid.Clear();
        garnishes.Clear();

        foreach (Transform child in garnishContainer)
            Destroy(child.gameObject);
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