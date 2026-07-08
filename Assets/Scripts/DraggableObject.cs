using UnityEngine;

public abstract class DraggableObject : InteractableObject
{
    private Vector3 startPosition;
    private Camera mainCamera;
    private bool dragging;
    [SerializeField] private bool canDrag;
    private float objectZ;
    [SerializeField] private LayerMask glassLayer;

    public bool CanDrag
    {
        get => canDrag;
        set => canDrag = value;
    }
    protected virtual void Awake()
    {
        startPosition = transform.position;
        mainCamera = Camera.main;
        objectZ = transform.position.z;
    }

    private void OnMouseDown()
    {
        if (!canDrag)
            return;
        dragging = true;
        OnDragStarted();
    }

    private void OnMouseDrag()
    {
        if (!dragging)
            return;

        Vector3 mouse = Input.mousePosition;
        mouse.z = Mathf.Abs(mainCamera.transform.position.z - objectZ);

        Vector3 world = mainCamera.ScreenToWorldPoint(mouse);

        world.z = objectZ;

        transform.position = world;
    }

    private void OnMouseUp()
    {
        dragging = false;

        OnDropped();

        ReturnHome();

    }

    protected virtual void ReturnHome()
    {
        transform.position = startPosition;
    }

    protected virtual void OnDragStarted()
    {

    }

    protected virtual void OnDropped()
    {

    }

    protected Glass GetTargetGlass(Vector3 point)
    {
        Collider2D hit = Physics2D.OverlapPoint(point, glassLayer);

        if (hit == null)
            return null;

        return hit.GetComponent<Glass>();
    }
}