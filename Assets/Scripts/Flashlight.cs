using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [Header("Flashlight Settings")]
    [SerializeField] private GameObject flashlightPrefab; // Prefab of the flashlight beam
    [SerializeField] private float flashDuration = 0.5f; // Duration the flashlight stays visible
    [SerializeField] private float flashOffset = 1f; // Distance from the player
    [SerializeField] private float beamLength = 2f; // Length of the flashlight beam
    [SerializeField] private float beamWidth = 0.5f; // Width of the flashlight beam

    private Movement playerMovement; // Reference to player's movement
    private Vector2 lastDirection = Vector2.down; // Default direction (facing down)

    void Start()
    {
        // Find the player's movement script
        playerMovement = GetComponent<Movement>();
    }

    void Update()
    {
        // Check if Space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireFlashlight();
        }

        // Update last direction based on player movement
        UpdateLastDirection();
    }

    private void UpdateLastDirection()
    {
        if (playerMovement != null)
        {
            Vector2 movement = new Vector2(playerMovement.HorizontalInput, playerMovement.VerticalInput);
            if (movement != Vector2.zero)
            {
                lastDirection = movement.normalized; // Update last direction
            }
        }
    }

    private void FireFlashlight()
    {
        Debug.Log("Firing flashlight...");
        
        // Instantiate the flashlight beam
        GameObject flashlight = Instantiate(flashlightPrefab, transform.position, Quaternion.identity);

        // Ensure flashlight is instantiated
        if (flashlight == null)
        {
            Debug.LogError("Flashlight prefab could not be instantiated!");
            return;
        }

        // Set the position and rotation
        flashlight.transform.position = (Vector2)transform.position + lastDirection * flashOffset;
        flashlight.transform.up = lastDirection; // Rotate to face the correct direction

        // Scale the beam length and width
        flashlight.transform.localScale = new Vector3(beamLength, beamWidth, flashlight.transform.localScale.z);

        // Ensure the flashlight collider is a trigger
        BoxCollider2D collider = flashlight.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }

        // Destroy the flashlight after the duration
        Destroy(flashlight, flashDuration);

        Debug.Log("Flashlight fired in direction: " + lastDirection);

        // Check for ghosts hit by the flashlight's collider
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(
            flashlight.transform.position,
            new Vector2(beamLength, beamWidth),
            Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg
        );

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.CompareTag("Ghost"))
            {
                Debug.Log("Ghost detected: " + hit.name);

                GhostController ghost = hit.GetComponent<GhostController>();
                if (ghost != null)
                {
                    Debug.Log("Fading and destroying ghost: " + hit.name);
                    ghost.FadeAndDestroy();
                }
                else
                {
                    Debug.LogWarning("GhostController not found on ghost: " + hit.name);
                    Destroy(hit.gameObject);
                }

                // Notify GameManager
                FindObjectOfType<GameManager>().GhostDefeated();
            }
        }
    }
}
