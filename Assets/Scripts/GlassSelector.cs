using UnityEngine;
using UnityEngine.UI;

public class GlassSelector : MonoBehaviour
{
    [SerializeField] private SpriteRenderer glassRenderer;
    [SerializeField] private SpriteRenderer backRenderer;
    [SerializeField] private SpriteRenderer liquidRenderer;
    [SerializeField] private GlassData[] glasses;

    private int currentIndex;

    private void Start()
    {
        UpdateGlass();
    }

    public void NextGlass()
    {
        currentIndex = (currentIndex + 1) % glasses.Length;
        UpdateGlass();
    }

    public void PreviousGlass()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = glasses.Length - 1;

        UpdateGlass();
    }

    private void UpdateGlass()
    {
        glassRenderer.sprite = glasses[currentIndex].front;
        backRenderer.sprite = glasses[currentIndex].back;       
        liquidRenderer.sprite = glasses[currentIndex].drink;
    }

    public GlassData CurrentGlass => glasses[currentIndex];
}