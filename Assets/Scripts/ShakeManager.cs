using UnityEngine;

public class ShakeManager : MiniGameManager
{
    [Header("Shake")]
    [SerializeField] private CocktailShaker shakerPrefab;

    private CocktailShaker currentShaker;
    public void StartShake()
    {
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
        orderManager.SetPreparationMethod(PreparationMethod.Shake);
        orderManager.SetMiniGameResult(success);
    }
}