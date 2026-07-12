using UnityEngine;

public class ShakeManager : MiniGameManager
{
    [Header("Shake")]
    [SerializeField] private CocktailShaker shakerPrefab;

    private CocktailShaker currentShaker;
    public void StartShake()
    {
        speedMeter.ConfigureSpeedRange(0f, 100f);
        speedMeter.SetSmoothTime(0.15f);
        StartMiniGame();
    }
    protected override void SpawnMiniGameObject()
    {
        currentShaker = Instantiate(
            shakerPrefab,
            currentGlass.transform.position,
            Quaternion.identity);
    }

    protected override float GetCurrentSpeed()
    {
        if (currentShaker == null)
            return 0f;

        return currentShaker.CurrentSpeed;
    }

    protected override void DestroyMiniGameObject()
    {
        if (currentShaker != null)
        {
            Destroy(currentShaker.gameObject);
            currentShaker = null;
        }
    }

    protected override string GetMiniGameName()
    {
        return "Shake";
    }

    protected override void OnMiniGameFinished(bool success)
    {
        // Aquí podremos avisar al PreparationManager
        // cuando implementemos el flujo completo.
    }
}