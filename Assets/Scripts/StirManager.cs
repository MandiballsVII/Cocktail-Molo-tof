using UnityEngine;

public class StirManager : MiniGameManager
{
    private StirSpoon currentSpoon;

    public void StartStir()
    {
        speedMeter.ConfigureSpeedRange(minSpeed, maxSpeed);
        speedMeter.SetSmoothTime(speedSmoothTime);
        StartMiniGame();
    }

    protected override void SpawnMiniGameObject()
    {
        currentSpoon = Instantiate(
            currentGlass.SpoonPrefab,
            currentGlass.SpoonSpawnPoint.position,
            Quaternion.identity);
    }

    protected override float GetCurrentSpeed()
    {
        if (currentSpoon == null)
            return 0f;

        return currentSpoon.CurrentSpeed;
    }

    protected override void DestroyMiniGameObject()
    {
        if (currentSpoon != null)
        {
            Destroy(currentSpoon.gameObject);
            currentSpoon = null;
        }
    }

    protected override string GetMiniGameName()
    {
        return "Stir";
    }

    protected override void OnMiniGameFinished(bool success)
    {
        // Más adelante avisaremos al PreparationManager.
    }
}