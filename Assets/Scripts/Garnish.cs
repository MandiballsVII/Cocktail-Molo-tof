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
        if(AudioManager.Instance != null)
            AudioManager.Instance.PlayOneShot(FMOD_Events.Instance.AgarrarBotella, Vector3.zero);
    }

    protected override void OnDragStarted()
    {
        base.OnDragStarted();
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayOneShot(FMOD_Events.Instance.PonerDecoracion, Vector3.zero);
    }
}