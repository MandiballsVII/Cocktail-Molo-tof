using UnityEngine;
using TMPro;

public class ShakeManager : MonoBehaviour
{
    [SerializeField] private CocktailShaker shakerPrefab;

    private CocktailShaker currentShaker;

    [SerializeField]
    private float shakeDuration = 10f;

    private float remainingTime;
    private bool timerRunning;

    public float RemainingTime => remainingTime;
    public float RemainingTimeNormalized =>
    remainingTime / shakeDuration;

    [SerializeField] private TMP_Text timerText;

    private Glass currentGlass;

    [SerializeField] private ShakeMeter shakeMeter;
    [SerializeField] private ShakeProgress progress;

    [Header("Progress Settings")]
    [SerializeField] private float greenProgressPerSecond = 0.35f;
    [SerializeField] private float yellowProgressPerSecond = 0.08f;
    [SerializeField] private float redPenaltyPerSecond = 0.25f;


    private void Update()
    {
        if (!timerRunning)
            return;

        remainingTime -= Time.deltaTime;
        UpdateTimerUI();

        if (currentShaker != null)
        {
            Debug.Log("Shaker speed: " + currentShaker.CurrentSpeed);
            shakeMeter.SetSpeed(currentShaker.CurrentSpeed);
        }
        if (shakeMeter.IsInGreenZone)
        {
            progress.AddProgress(
                greenProgressPerSecond * Time.deltaTime);
        }
        else if (shakeMeter.IsInYellowZone)
        {
            progress.AddProgress(
                yellowProgressPerSecond * Time.deltaTime);
        }
        else
        {
            progress.AddProgress(
                -redPenaltyPerSecond * Time.deltaTime);
        }
        if (remainingTime <= 0f)
        {
            remainingTime = 0f;

            UpdateTimerUI();

            FinishShake();
        }
    }
    public void StartShake(Glass glass)
    {
        currentGlass = glass;
        shakeMeter.ResetMeter();
        shakeMeter.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        currentGlass.Liquid.Hide();
        currentShaker = Instantiate(
            shakerPrefab,
            glass.transform.position,
            Quaternion.identity);
        remainingTime = shakeDuration;
        timerRunning = true;
        UpdateTimerUI();
    }

    public void EndShake()
    {
        if (currentGlass != null)
        {
            currentGlass.Liquid.Show();
        }
        if (currentShaker != null)
            Destroy(currentShaker.gameObject);

        currentGlass = null;
        currentShaker = null;
        shakeMeter.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
    }
    private void FinishShake()
    {
        timerRunning = false;

        EndShake();

        // Más adelante:
        // PreparationManager.OnShakeFinished(success);
    }
    private void UpdateTimerUI()
    {
        timerText.text = remainingTime.ToString("F2");
    }
}