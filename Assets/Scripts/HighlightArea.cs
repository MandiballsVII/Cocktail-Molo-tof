using UnityEngine;

public class HighlightArea : MonoBehaviour
{
    [Header("Renderers")]
    [SerializeField] private SpriteRenderer innerGlow;
    [SerializeField] private SpriteRenderer outerGlow;

    [Header("Pulse")]
    [SerializeField] private float pulseSpeed = 1.4f;
    [SerializeField] private float pulseScale = 0.03f;

    [Header("Alpha")]
    [SerializeField] private float innerAlpha = 0.22f;
    [SerializeField] private float outerAlpha = 0.10f;
    [SerializeField] private float alphaVariation = 0.04f;

    private Vector3 initialScale;

    private bool visible;

    void Awake()
    {
        initialScale = transform.localScale;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!visible)
            return;

        float wave = Mathf.Sin(Time.time * pulseSpeed);

        transform.localScale =
            initialScale * (1 + wave * pulseScale);

        Color c = innerGlow.color;
        c.a = innerAlpha + wave * alphaVariation;
        innerGlow.color = c;

        c = outerGlow.color;
        c.a = outerAlpha + wave * alphaVariation * 0.5f;
        outerGlow.color = c;
    }

    public void Show()
    {
        initialScale = transform.localScale;
        visible = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        visible = false;
        gameObject.SetActive(false);
    }
}