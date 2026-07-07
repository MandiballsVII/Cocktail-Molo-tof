using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] private SpriteRenderer liquidRenderer;

    public void Pour(BottleData bottle)
    {
        liquidRenderer.color = bottle.liquidColor;

        Debug.Log("Añadido " + bottle.bottleName);
    }
}