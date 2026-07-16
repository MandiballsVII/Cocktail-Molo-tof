using UnityEngine;
using UnityEngine.UI;

public class DrunkennessBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    [SerializeField] private int maxPoints = 10;

    public void SetPoints(int points)
    {
        points = Mathf.Clamp(points, 0, maxPoints);

        fillImage.fillAmount =
            (float)points / maxPoints;
    }

    public void ResetBar()
    {
        SetPoints(0);
    }
}