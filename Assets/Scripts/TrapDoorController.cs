using UnityEngine;

public class TrapdoorController : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float openAngle = 90f; // Angle to rotate when opening
    public float rotationSpeed = 90f; // Degrees per second

    [Header("State")]
    public bool isOpen = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private float rotationProgress = 0f;

    void Start()
    {
        // Store initial rotation as closed position
        closedRotation = transform.rotation;

        // Calculate open rotation (rotating around the X-axis in this example)
        openRotation = closedRotation * Quaternion.Euler(0, 0, openAngle);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleTrapdoor();
        }

        // Smoothly rotate between open and closed states
        if (isOpen)
        {
            rotationProgress = Mathf.Min(rotationProgress + Time.deltaTime * rotationSpeed / openAngle, 1f);
        }
        else
        {
            rotationProgress = Mathf.Max(rotationProgress - Time.deltaTime * rotationSpeed / openAngle, 0f);
        }

        transform.rotation = Quaternion.Slerp(closedRotation, openRotation, rotationProgress);
    }

    // Public method to toggle the trapdoor
    public void ToggleTrapdoor()
    {
        isOpen = !isOpen;
    }
}