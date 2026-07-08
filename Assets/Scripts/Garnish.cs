using UnityEngine;

public class Garnish : DraggableObject
{
    [SerializeField] private GarnishData data;

    [SerializeField] private Transform interactionPoint;

    protected override void OnDropped()
    {
        Glass glass = GetTargetGlass(interactionPoint.position);

        if (glass == null)
            return;

        glass.AddGarnish(data);
    }
}