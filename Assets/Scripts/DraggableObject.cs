using UnityEngine;

public abstract class DraggableObject : InteractableObject
{
    private Vector3 startPosition;
    private Camera mainCamera;
    private bool dragging;
    [SerializeField] private bool canDrag;

    public bool CanDrag
    {
        get => canDrag;
        set => canDrag = value;
    }
    protected virtual void Awake()
    {
        startPosition = transform.position;
        mainCamera = Camera.main;
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
        mouse.z = Mathf.Abs(mainCamera.transform.position.z);

        transform.position = mainCamera.ScreenToWorldPoint(mouse);
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
}