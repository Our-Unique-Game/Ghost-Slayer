using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public bool CanMove = true; // Controls whether the player can move (useful for interactions)

    public float HorizontalInput { get; private set; } // Tracks horizontal movement input
    public float VerticalInput { get; private set; } // Tracks vertical movement input

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

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

        // Apply movement to Rigidbody
        rb.linearVelocity = movement * speed;

        // Update Animator parameters
        animator.SetFloat("Speed", rb.linearVelocity.magnitude); // Overall movement speed
        animator.SetFloat("Horizontal", movement.x); // Horizontal direction (-1, 0, 1)
        animator.SetFloat("Vertical", movement.y); // Vertical direction (-1, 0, 1)

        // Handle sprite flipping for left movement
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false; // Face right
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true; // Face left
        }
    }
}
