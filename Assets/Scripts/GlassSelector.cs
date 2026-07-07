using UnityEngine;

public class GlassSelector : MonoBehaviour
{
    [SerializeField] private SpriteRenderer glassRenderer;
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
        glassRenderer.sprite = glasses[currentIndex].sprite;
    }

    public GlassData CurrentGlass => glasses[currentIndex];
}