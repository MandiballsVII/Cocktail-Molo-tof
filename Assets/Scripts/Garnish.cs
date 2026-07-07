using UnityEngine;

public class Garnish : DraggableObject
{
    [SerializeField] private GarnishData data;

    [SerializeField] private Transform dropPoint;

    protected override void OnDropped()
    {
        Collider2D hit = Physics2D.OverlapPoint(dropPoint.position);

        if (hit == null)
            return;

        Glass glass = hit.GetComponent<Glass>();

        if (glass != null)
        {
            glass.AddGarnish(data);
        }
    }
}