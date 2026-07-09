using UnityEngine;

public class ShakeMeter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private RectTransform arrow;

    [Header("Bar Limits")]
    [SerializeField] private float minY = -180f;
    [SerializeField] private float maxY = 180f;

    [Header("Speed")]
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float maxSpeed = 10f;

    [Header("Smoothing")]
    [SerializeField] private float smoothTime = 0.15f;

    [Header("Green Zone")]
    [Range(0f, 1f)]
    [SerializeField] private float greenMin = 0.4f;

    [Range(0f, 1f)]
    [SerializeField] private float greenMax = 0.6f;

    [Header("Yellow Zone")]
    [SerializeField]
    private float yellowMin = 0.30f;

    [SerializeField]
    private float yellowMax = 0.70f;

    public bool IsInYellowZone { get; }

    public bool IsInRedZone { get; }

    private float targetNormalizedValue;
    private float currentNormalizedValue;
    private float smoothVelocity;

    /// <summary>
    /// Valor que representa la posición ACTUAL de la flecha (0-1).
    /// Este es el valor que deberá consultar el ShakeManager.
    /// </summary>
    public float CurrentNormalizedValue => currentNormalizedValue;

    /// <summary>
    /// Indica si la flecha está actualmente dentro de la zona verde.
    /// </summary>
    public bool IsInGreenZone =>
        currentNormalizedValue >= greenMin &&
        currentNormalizedValue <= greenMax;

    /// <summary>
    /// Recibe la velocidad de la coctelera.
    /// </summary>
    public void SetSpeed(float speed)
    {
        targetNormalizedValue = Mathf.Clamp01(Mathf.InverseLerp(minSpeed, maxSpeed, speed));
    }

    private void Update()
    {
        currentNormalizedValue = Mathf.SmoothDamp(
            currentNormalizedValue,
            targetNormalizedValue,
            ref smoothVelocity,
            smoothTime);

        float y = Mathf.Lerp(
            minY,
            maxY,
            currentNormalizedValue);

        Vector2 pos = arrow.anchoredPosition;
        pos.y = y;
        arrow.anchoredPosition = pos;
    }

    /// <summary>
    /// Reinicia la barra al comenzar un nuevo minijuego.
    /// </summary>
    public void ResetMeter()
    {
        targetNormalizedValue = 0f;
        currentNormalizedValue = 0f;
        smoothVelocity = 0f;

        Vector2 pos = arrow.anchoredPosition;
        pos.y = minY;
        arrow.anchoredPosition = pos;
    }
}