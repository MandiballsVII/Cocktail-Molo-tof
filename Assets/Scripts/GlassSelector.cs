using UnityEngine;
using UnityEngine.UI;

public class GlassSelector : MonoBehaviour
{
    [SerializeField] private SpriteRenderer glassRenderer;
    [SerializeField] private GlassData[] glasses;
    [SerializeField] private Button[] selectionButtons; 

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

    public void ConfirmSelection()
    {
        foreach (var button in selectionButtons)
        {
            button.interactable = false;
        }
    }

    public GlassData CurrentGlass => glasses[currentIndex];
}