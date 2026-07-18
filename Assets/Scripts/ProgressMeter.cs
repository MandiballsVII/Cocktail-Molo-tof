using UnityEngine;
using UnityEngine.UI;

public class ProgressMeter : MonoBehaviour
{
    [SerializeField] private Image progressImage;

    private float progress;

    public float Progress => progress;

    public bool IsCompleted => progress >= 1f;

    public void SetCocktailSprite(Sprite sprite)
    {
        progressImage.sprite = sprite;
    }
    public void ResetProgress()
    {
        progress = 0f;
        progressImage.fillAmount = 0f;
    }

    public void AddProgress(float amount)
    {
        progress = Mathf.Clamp01(progress + amount);

        progressImage.fillAmount = progress;
    }
}