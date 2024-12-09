using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
    [Header("Teleport Settings")]
    public GameObject destination; // Destination object (target door/ladder)

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
            // Get the destination's SpriteRenderer center
            SpriteRenderer spriteRenderer = destination.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // Get the center of the destination's sprite bounds
                Vector3 centerPosition = spriteRenderer.bounds.center;

                // Teleport the player to the center position
                GameObject player = GameObject.FindWithTag("Player");
                player.transform.position = centerPosition;
            }
            else
            {
                Debug.LogWarning("Destination does not have a SpriteRenderer: " + destination.name);
            }
        }
        else
        {
            Debug.LogWarning("No destination assigned on " + gameObject.name);
        }
    }
}
