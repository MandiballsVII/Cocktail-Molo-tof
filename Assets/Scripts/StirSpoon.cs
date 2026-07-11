using UnityEngine;

public class StirSpoon : MonoBehaviour
{
    private Camera mainCamera;

    private bool dragging;

    private float objectZ;

    private Vector3 offset;

    private bool insideGlass;

    private Glass currentGlass;

    private float fixedY;

    private Vector3 previousPosition;

    private float currentSpeed;

    [SerializeField]
    private float speedSmooth = 8f;

    private float filteredSpeed;

    public float CurrentSpeed => filteredSpeed;

    private void Awake()
    {
        mainCamera = Camera.main;

        objectZ = transform.position.z;

        previousPosition = transform.position;
    }

    private void Update()
    {
        filteredSpeed = Mathf.Lerp(
            filteredSpeed,
            currentSpeed,
            Time.deltaTime * speedSmooth);
    }

    private void OnMouseDown()
    {
        dragging = true;

        Vector3 mouse = Input.mousePosition;
        mouse.z = Mathf.Abs(mainCamera.transform.position.z - objectZ);

        offset = transform.position -
                 mainCamera.ScreenToWorldPoint(mouse);
    }

    private void OnMouseDrag()
    {
        if (!dragging)
            return;

        Vector3 mouse = Input.mousePosition;
        mouse.z = Mathf.Abs(mainCamera.transform.position.z - objectZ);

        Vector3 world = mainCamera.ScreenToWorldPoint(mouse);

        world += offset;

        world.z = objectZ;

        if (!insideGlass)
        {
            transform.position = world;
        }
        else
        {
            world.y = fixedY;

            float x = Mathf.Clamp(
                world.x,
                currentGlass.LeftLimit.position.x,
                currentGlass.RightLimit.position.x);

            world.x = x;

            transform.position = world;
        }

        float deltaX = transform.position.x - previousPosition.x;

        currentSpeed = Mathf.Abs(deltaX) / Time.deltaTime;

        previousPosition = transform.position;
    }

    private void OnMouseUp()
    {
        dragging = false;

        currentSpeed = 0;
    }

    public void OnTipEntered(Collider2D other)
    {
        if (insideGlass)
            return;

        Glass glass = other.GetComponentInParent<Glass>();

        if (glass == null)
            return;

        insideGlass = true;
        currentGlass = glass;
        fixedY = transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (insideGlass)
            return;

        Glass glass = other.GetComponentInParent<Glass>();

        if (glass == null)
            return;

        insideGlass = true;

        currentGlass = glass;

        fixedY = transform.position.y;
    }
}