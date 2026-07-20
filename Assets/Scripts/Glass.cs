using System;
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
    [SerializeField] private Collider2D glassCollider;

    [Header("Garnishes")]

    [SerializeField] private Transform featherPoint;

    [SerializeField] private Transform garnishTopLeft;

    [SerializeField] private Transform garnishTopRight;

    [SerializeField] private Transform garnishBottomLeft;

    [SerializeField] private Transform garnishBottomRight;

    public Transform FeatherPoint => featherPoint;

    public Transform GarnishTopLeft => garnishTopLeft;
    public Transform GarnishTopRight => garnishTopRight;
    public Transform GarnishBottomLeft => garnishBottomLeft;
    public Transform GarnishBottomRight => garnishBottomRight;

    public Transform LeftLimit => leftLimit;
    public Transform RightLimit => rightLimit;
    public Collider2D StirZone => stirZone;

    [SerializeField] private StirSpoon spoonPrefab;

    public StirSpoon SpoonPrefab => spoonPrefab;

    [SerializeField] private Transform spoonSpawnPoint;

    public Transform SpoonSpawnPoint => spoonSpawnPoint;

    [SerializeField] private GlassData data;

    public GlassData Data => data;

    public Liquid Liquid => liquid;

    public event Action FirstIngredientAdded;

    public void Pour(BottleData bottle)
    {
        bool wasEmpty = liquid.TotalUnits == 0;

        liquid.Show();
        liquid.Pour(bottle);

        if (wasEmpty)
            FirstIngredientAdded?.Invoke();
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

        GameObject garnishObject =
            Instantiate(garnish.prefab, garnishContainer);

        float finalScale =
            garnish.baseScale *
            data.garnishScaleMultiplier;

        garnishObject.transform.localScale =
            Vector3.one * finalScale;

        Garnish garnishComponent =
            garnishObject.GetComponent<Garnish>();

        if (garnish.placement == GarnishPlacement.Surface)
        {
            PlaceSurfaceGarnish(garnishObject, garnishComponent);
        }
        else
        {
            PlaceFloatingGarnish(garnishObject);
        }
    }

    private void PlaceFloatingGarnish(GameObject garnishObject)
    {
        float x =
            UnityEngine.Random.Range(
                garnishTopLeft.localPosition.x,
                garnishTopRight.localPosition.x);

        float y =
            UnityEngine.Random.Range(
                garnishBottomLeft.localPosition.y,
                garnishTopLeft.localPosition.y);

        garnishObject.transform.localPosition =
            new Vector3(x, y, 0);

        garnishObject.transform.localRotation =
            Quaternion.Euler(
                0,
                0,
                UnityEngine.Random.Range(-30f, 30f));
    }

    private void PlaceSurfaceGarnish(
        GameObject garnishObject,
        Garnish garnish)
    {
        Transform interactionPoint =
            garnish.InteractionPoint;

        Vector3 worldOffset =
            garnishObject.transform.position -
            interactionPoint.position;

        garnishObject.transform.position =
            featherPoint.position +
            worldOffset;

        garnishObject.transform.rotation =
            Quaternion.identity;
    }
    public void SetInteractionEnabled(bool enabled)
    {
        glassCollider.enabled = enabled;
    }
}