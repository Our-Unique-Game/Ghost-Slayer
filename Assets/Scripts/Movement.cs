using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input for horizontal and vertical movement
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Move the player
        rb.linearVelocity = new Vector2(moveX * speed, moveY * speed);

        // Update Animator parameters
        animator.SetFloat("Speed", Mathf.Abs(moveX) + Mathf.Abs(moveY)); // Speed parameter for transitions
        animator.SetFloat("Horizontal", moveX); // Direction on X-axis
        animator.SetFloat("Vertical", moveY);   // Direction on Y-axis

        // Set player facing direction (idle direction based on last movement)
        if (moveX != 0 || moveY != 0)
        {
            animator.SetFloat("LastMoveX", moveX);
            animator.SetFloat("LastMoveY", moveY);
        }
    }
}
