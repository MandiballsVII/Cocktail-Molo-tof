using UnityEngine;

public class StirSpoonTip : MonoBehaviour
{
    [SerializeField] private StirSpoon spoon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        spoon.OnTipEntered(other);
    }
}