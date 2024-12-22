using UnityEngine;

public class CameraBoundCursor : MonoBehaviour
{
    [Header("Cursor Settings")]
    [SerializeField]
    private bool showCursor = true;

    [SerializeField]
    private bool lockToWindow = true;

    [SerializeField]
    private float cursorSpeed = 1f;

    private Camera mainCamera;
    private Vector3 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        mainCamera = Camera.main;

        // Get object's sprite dimensions if it has a sprite renderer
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sprite))
        {
            objectWidth = sprite.bounds.extents.x;
            objectHeight = sprite.bounds.extents.y;
        }
        else
        {
            // Default size if no sprite renderer
            objectWidth = transform.localScale.x / 2;
            objectHeight = transform.localScale.y / 2;
        }

        // Set cursor visibility
        Cursor.visible = showCursor;

        // Lock cursor to game window if specified
        if (lockToWindow)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    void Update()
    {
        // Get mouse position in world space
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Keep z position constant

        // Get screen bounds in world coordinates
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Calculate the bounds considering object size
        float minX = -screenBounds.x + objectWidth;
        float maxX = screenBounds.x - objectWidth;
        float minY = -screenBounds.y + objectHeight;
        float maxY = screenBounds.y - objectHeight;

        // Clamp position within camera bounds
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(mousePos.x, minX, maxX),
            Mathf.Clamp(mousePos.y, minY, maxY),
            0f
        );

        // Move object to clamped position
        transform.position = Vector3.Lerp(
            transform.position,
            clampedPosition,
            Time.deltaTime * cursorSpeed * 10f
        );
    }

    // Optional: Method to toggle cursor visibility
    public void SetCursorVisibility(bool visible)
    {
        showCursor = visible;
        Cursor.visible = visible;
    }
}
