using UnityEngine;

public class Bottle : DraggableObject
{
    [SerializeField] private BottleData data;

    [SerializeField] private Transform interactionPoint;

    protected override void OnDropped()
    {
        Glass glass = GetTargetGlass(interactionPoint.position);

        if (glass == null)
            return;

        glass.Pour(data);
    }
}