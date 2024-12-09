using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
    [Header("Teleport Settings")]
    [SerializeField] private GameObject destination; // Destination object (target door/ladder)

    private bool isPlayerInRange = false; // Tracks if the player is near this object

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        if (destination != null)
        {
            // Teleport player to destination
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                // Get the center of the destination's sprite bounds
                SpriteRenderer spriteRenderer = destination.GetComponent<SpriteRenderer>();
                Vector3 targetPosition = spriteRenderer != null ? spriteRenderer.bounds.center : destination.transform.position;

                // Move the player
                player.transform.position = targetPosition;
            }
            else
            {
                Debug.LogWarning("Player object not found!");
            }
        }
        else
        {
            Debug.LogWarning("No destination assigned on " + gameObject.name);
        }
    }
}
