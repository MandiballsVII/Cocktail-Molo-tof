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
    private float outputSmooth = 8f;

    private float filteredSpeed;

    public float CurrentSpeed => filteredSpeed;

    [SerializeField]
    private float movementSmooth = 12f;

    private int previousDirection = 0;
    [SerializeField] private float directionThreshold = 0.05f;

    private void Awake()
    {
        mainCamera = Camera.main;

        objectZ = transform.position.z;

        previousPosition = transform.position;
    }

    private void Update()
    {
        float targetSpeed = insideGlass ? currentSpeed : 0f;

        filteredSpeed = Mathf.Lerp(
            filteredSpeed,
            targetSpeed,
            Time.deltaTime * outputSmooth);
    }

    private void OnMouseDown()
    {
        previousDirection = 0;
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
        if (insideGlass)
        {

            float rawSpeed = Mathf.Abs(deltaX) / Time.deltaTime;

            currentSpeed = Mathf.Lerp(
                currentSpeed,
                rawSpeed,
                Time.deltaTime * movementSmooth);
        }
        else
        {
            currentSpeed = 0f;
        }

        previousPosition = transform.position;

        int currentDirection = 0;

        if (deltaX > directionThreshold)
            currentDirection = 1;
        else if (deltaX < -directionThreshold)
            currentDirection = -1;

        if (currentDirection != 0 &&
            previousDirection != 0 &&
            currentDirection != previousDirection)
        {
            AudioManager.Instance.PlayOneShot(
                FMOD_Events.Instance.CucharaRemover,
                transform.position);
        }

        if (currentDirection != 0)
            previousDirection = currentDirection;
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
    
}