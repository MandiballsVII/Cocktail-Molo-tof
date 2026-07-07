using UnityEngine;

public class Bottle : DraggableObject
{
    [SerializeField] private BottleData data;

    [SerializeField] private Transform pourPoint;

    protected override void OnDropped()
    {
        Collider2D hit = Physics2D.OverlapPoint(pourPoint.position);

        if (hit == null)
            return;

        Glass glass = hit.GetComponent<Glass>();

        if (glass != null)
        {
            glass.Pour(data);
        }
    }
}