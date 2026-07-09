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

    private void Update()
    {
        if (!timerRunning)
            return;

        remainingTime -= Time.deltaTime;
        UpdateTimerUI();

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            timerRunning = false;

            remainingTime = 0;

            UpdateTimerUI();

            FinishShake();
        }
    }
    public void StartShake(Glass glass)
    {
        currentGlass = glass;

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
        {
            Destroy(currentShaker.gameObject);
        }

        currentGlass = null;
        currentShaker = null;
        timerText.gameObject.SetActive(false);
        if (currentShaker != null)
            Destroy(currentShaker.gameObject);
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