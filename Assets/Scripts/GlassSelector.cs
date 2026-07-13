using UnityEngine;

public class GlassSelector : MonoBehaviour
{
    [SerializeField] private GlassData[] glasses;

    [SerializeField] private Transform spawnPoint;

    private int currentIndex;

    private Glass currentGlass;

    public Glass CurrentGlass => currentGlass;

    private void Start()
    {
        SpawnGlass();
    }

    public void NextGlass()
    {
        currentIndex++;

        if (currentIndex >= glasses.Length)
            currentIndex = 0;

        SpawnGlass();
    }

    public void PreviousGlass()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = glasses.Length - 1;

        SpawnGlass();
    }
    public void ResetSelection()
    {
        currentIndex = 0;

        SpawnGlass();
    }

    private void SpawnGlass()
    {
        if (currentGlass != null)
            Destroy(currentGlass.gameObject);

        currentGlass = Instantiate(
            glasses[currentIndex].prefab,
            spawnPoint.position,
            spawnPoint.rotation,
            spawnPoint);
    }
}