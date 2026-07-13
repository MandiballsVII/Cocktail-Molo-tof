using TMPro;
using UnityEngine;

public abstract class MiniGameManager : MonoBehaviour
{
    [Header("Common References")]
    [SerializeField] protected GlassSelector glassSelector;
    [SerializeField] protected SpeedMeter speedMeter;
    [SerializeField] protected ProgressMeter progress;
    [SerializeField] protected TMP_Text timerText;
    [SerializeField] protected OrderManager orderManager;

    [Header("Timer")]
    [SerializeField]
    protected float miniGameDuration = 10f;

    protected float remainingTime;
    protected bool timerRunning;

    protected Glass currentGlass;

    [Header("Progress Settings")]
    [SerializeField]
    protected float greenProgressPerSecond = 0.35f;

    [SerializeField]
    protected float yellowProgressPerSecond = 0.08f;

    [SerializeField]
    protected float redPenaltyPerSecond = 0.25f;

    [Header("Speed Settings")]
    [SerializeField] protected float minSpeed = 0f;
    [SerializeField] protected float maxSpeed = 100f;
    [SerializeField] protected float speedSmoothTime = 0.15f;

    protected virtual void Update()
    {
        if (!timerRunning)
            return;

        remainingTime -= Time.deltaTime;

        UpdateTimerUI();

        speedMeter.SetSpeed(GetCurrentSpeed());

        if (speedMeter.IsInGreenZone)
        {
            progress.AddProgress(
                greenProgressPerSecond * Time.deltaTime);
        }
        else if (speedMeter.IsInYellowZone)
        {
            progress.AddProgress(
                yellowProgressPerSecond * Time.deltaTime);
        }
        else
        {
            progress.AddProgress(
                -redPenaltyPerSecond * Time.deltaTime);
        }

        if (progress.IsCompleted)
        {
            FinishMiniGame(true);
            return;
        }

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;

            UpdateTimerUI();

            FinishMiniGame(false);
        }
    }

    protected void StartMiniGame()
    {
        currentGlass = glassSelector.CurrentGlass;

        if (currentGlass == null)
            return;

        progress.ResetProgress();

        speedMeter.ConfigureSpeedRange(minSpeed, maxSpeed);
        speedMeter.ResetMeter();

        speedMeter.ConfigureSpeedRange(minSpeed, maxSpeed);
        speedMeter.SetSmoothTime(speedSmoothTime);

        progress.gameObject.SetActive(true);
        speedMeter.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);

        SpawnMiniGameObject();

        remainingTime = miniGameDuration;
        timerRunning = true;

        UpdateTimerUI();
    }

    protected void FinishMiniGame(bool success)
    {
        timerRunning = false;

        if (currentGlass != null)
            currentGlass.Liquid.Show();

        progress.gameObject.SetActive(false);
        speedMeter.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

        DestroyMiniGameObject();

        currentGlass = null;

        Debug.Log(success
            ? $"{GetMiniGameName()} completado"
            : $"{GetMiniGameName()} fallido");

        OnMiniGameFinished(success);
    }

    protected void UpdateTimerUI()
    {
        timerText.text = remainingTime.ToString("F2");
    }

    /// <summary>
    /// Devuelve la velocidad actual de la herramienta.
    /// </summary>
    protected abstract float GetCurrentSpeed();

    /// <summary>
    /// Destruye la herramienta (coctelera, cuchara...).
    /// </summary>
    protected abstract void DestroyMiniGameObject();

    /// <summary>
    /// Nombre para el Debug.
    /// </summary>
    protected abstract string GetMiniGameName();

    /// <summary>
    /// Permite que cada hijo haga algo al terminar.
    /// </summary>
    protected virtual void OnMiniGameFinished(bool success)
    {
    }

    protected abstract void SpawnMiniGameObject();
}