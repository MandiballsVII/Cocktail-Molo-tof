using UnityEngine;
using TMPro;

public class StirManager : MonoBehaviour
{
    [SerializeField] private GlassSelector glassSelector;

    [SerializeField] private SpeedMeter speedMeter;
    [SerializeField] private ProgressMeter progress;

    [SerializeField] private TMP_Text timerText;

    [SerializeField]
    private float stirDuration = 10f;

    private float remainingTime;
    private bool timerRunning;

    private Glass currentGlass;
    private StirSpoon currentSpoon;

    private void Update()
    {
        if (!timerRunning)
            return;

        remainingTime -= Time.deltaTime;

        timerText.text = remainingTime.ToString("F2");

        if (remainingTime <= 0f)
        {
            FinishStir(false);
        }
    }

    public void StartStir()
    {
        currentGlass = glassSelector.CurrentGlass;

        if (currentGlass == null)
            return;

        speedMeter.ResetMeter();
        progress.ResetProgress();

        speedMeter.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);

        currentGlass.Liquid.Hide();

        currentSpoon = Instantiate(
            currentGlass.SpoonPrefab,
            currentGlass.SpoonSpawnPoint.position,
            Quaternion.identity);

        remainingTime = stirDuration;
        timerRunning = true;

        timerText.text = remainingTime.ToString("F2");
    }

    private void FinishStir(bool success)
    {
        timerRunning = false;

        if (currentGlass != null)
            currentGlass.Liquid.Show();

        if (currentSpoon != null)
            Destroy(currentSpoon.gameObject);

        speedMeter.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

        Debug.Log(success ? "Stir completado" : "Stir fallido");
    }
}