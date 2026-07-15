using UnityEngine;

public class Garnish : DraggableObject
{
    [SerializeField] private GarnishData data;

    [SerializeField] private Transform interactionPoint;
    public Transform InteractionPoint => interactionPoint;

    protected override void OnDropped()
    {
        Glass glass = GetTargetGlass(interactionPoint.position);

        if (glass == null)
            return;

        glass.AddGarnish(data);
    }
}