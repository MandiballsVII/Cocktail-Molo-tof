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
        if(AudioManager.Instance != null)
            AudioManager.Instance.PlayOneShot(FMOD_Events.Instance.VerterLiquido, Vector3.zero);
    }
    protected override void OnDragStarted()
    {
        base.OnDragStarted();
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayOneShot(FMOD_Events.Instance.AgarrarBotella, Vector3.zero);
    }
}