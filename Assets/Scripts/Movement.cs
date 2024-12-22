using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] public float speed = 5f; // Movement speed
    [SerializeField] public bool CanMove = true; // Controls whether the player can move (useful for interactions)

    [SerializeField] public float HorizontalInput { get; private set; } // Tracks horizontal movement input
    [SerializeField] public float VerticalInput { get; private set; } // Tracks vertical movement input

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Variables for direction handling
    private Vector2 lastDirection = Vector2.down; // Default facing down
    private Vector2 currentDirection; // Current direction based on movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Prevent movement if disabled
        if (!CanMove)
        {
            rb.linearVelocity = Vector2.zero; // Stop all movement
            animator.SetFloat("Speed", 0f); // Ensure Idle animation is played
            return;
        }

        // Get movement input
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        // Normalize movement for consistent speed in all directions
        Vector2 movement = new Vector2(HorizontalInput, VerticalInput).normalized;

        // *** PRIORITY FIX: Update last direction only if a new key is pressed ***
        if (movement != Vector2.zero) // Player is moving
        {
            // Prioritize Horizontal movement over Vertical
            if (Mathf.Abs(HorizontalInput) > Mathf.Abs(VerticalInput)) // Horizontal is stronger
            {
                currentDirection = new Vector2(HorizontalInput, 0); // Horizontal direction
            }
            else // Vertical is stronger
            {
                currentDirection = new Vector2(0, VerticalInput); // Vertical direction
            }

            // Update last direction only when input changes
            if (currentDirection != lastDirection)
            {
                lastDirection = currentDirection; // Save direction
            }
        }

        // Apply movement to Rigidbody
        rb.linearVelocity = movement * speed;

        // Update Animator parameters
        animator.SetFloat("Speed", rb.linearVelocity.magnitude); // Overall movement speed
        animator.SetFloat("Horizontal", lastDirection.x); // Use last direction for horizontal
        animator.SetFloat("Vertical", lastDirection.y);   // Use last direction for vertical

        // Handle sprite flipping for left movement
        if (lastDirection.x > 0)
        {
            spriteRenderer.flipX = false; // Face right
        }
        else if (lastDirection.x < 0)
        {
            spriteRenderer.flipX = true; // Face left
        }
    }
}
