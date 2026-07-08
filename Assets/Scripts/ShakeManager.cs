using UnityEngine;

public class ShakeManager : MonoBehaviour
{
    [SerializeField] private CocktailShaker shakerPrefab;

    private CocktailShaker currentShaker;

    public void StartShake(Glass glass)
    {
        glass.Liquid.Hide();
        currentShaker = Instantiate(
            shakerPrefab,
            glass.transform.position,
            Quaternion.identity);
    }

    public void EndShake()
    {
        if (currentShaker != null)
            Destroy(currentShaker.gameObject);
    }
}