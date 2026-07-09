using UnityEngine;

public class CocktailShaker : MonoBehaviour
{
    private Camera mainCamera;
    private bool dragging;
    private float objectZ;
    private Vector3 offset;

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
        filteredSpeed = Mathf.Lerp(filteredSpeed, currentSpeed, Time.deltaTime * speedSmooth);
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
        {
            currentSpeed = 0;
            previousPosition = transform.position;
            return;
        }

        Vector3 mouse = Input.mousePosition;
        mouse.z = Mathf.Abs(mainCamera.transform.position.z - objectZ);

        Vector3 world = mainCamera.ScreenToWorldPoint(mouse);

        world.z = objectZ;

        transform.position = world + offset;

        float deltaY = transform.position.y - previousPosition.y;

        currentSpeed = Mathf.Abs(deltaY) / Time.deltaTime;

        previousPosition = transform.position;

        print($"Current Speed: {currentSpeed}");
        print($"Filtered Speed: {filteredSpeed}");
    }

    private void OnMouseUp()
    {
        dragging = false;
        currentSpeed = 0;
    }
}