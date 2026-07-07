using UnityEngine;

public class Bottle : DraggableObject
{
    [SerializeField] private BottleData data;

    protected override void OnDropped()
    {
        Collider2D hit = Physics2D.OverlapPoint(transform.position);

        if (hit == null)
            return;

        Glass glass = hit.GetComponent<Glass>();

        if (glass != null)
        {
            glass.Pour(data);
        }
    }
}