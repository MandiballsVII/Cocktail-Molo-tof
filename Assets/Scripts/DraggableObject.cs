using UnityEngine;

public abstract class DraggableObject : InteractableObject
{
    private Vector3 startPosition;
    private Camera mainCamera;
    private bool dragging;

    protected virtual void Awake()
    {
        startPosition = transform.position;
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
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