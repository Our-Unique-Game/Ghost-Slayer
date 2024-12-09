using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
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
        // Get movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Normalize movement to prevent faster diagonal movement
        Vector2 movement = new Vector2(moveX, moveY).normalized;

        // Apply movement
        rb.linearVelocity = movement * speed;

        // Update Animator parameters
        animator.SetFloat("Speed", rb.linearVelocity.magnitude); // Overall speed
        animator.SetFloat("Horizontal", movement.x);       // Horizontal direction
        animator.SetFloat("Vertical", movement.y);         // Vertical direction

        // Flip sprite for leftward movement
        if (movement.x > 0)
            spriteRenderer.flipX = false; // Face right
        else if (movement.x < 0)
            spriteRenderer.flipX = true; // Face left
    }
}
