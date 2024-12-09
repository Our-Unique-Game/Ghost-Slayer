using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Movement speed

    [Header("Directional Sprites")]
    public Sprite leftSprite; // Sprite for moving left
    public Sprite rightSprite; // Sprite for moving right

    [Header("Sprite Sizes")]
    [SerializeField] private Vector2 leftSpriteSize = Vector2.one; // Size of the left sprite
    [SerializeField] private Vector2 rightSpriteSize = Vector2.one; // Size of the right sprite

    private Vector2 movement; // Movement vector
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    // Movement enabled flag
    public bool CanMove { get; set; } = true;

    private void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!CanMove) // Disable input if movement is not allowed
        {
            movement = Vector2.zero;
            return;
        }

        // Get input for horizontal (A/D or Left/Right) and vertical (W/S or Up/Down) movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize the movement vector for consistent speed
        movement = movement.normalized;

        // Update the sprite based on the direction
        UpdateSprite();
    }

    private void FixedUpdate()
    {
        // Move the character using Rigidbody2D
        rb.linearVelocity = movement * speed;
    }

    private void UpdateSprite()
    {
        // Update the sprite and size based on the direction of movement
        if (movement.x > 0) // Moving right
        {
            spriteRenderer.sprite = rightSprite;
            spriteRenderer.transform.localScale = new Vector3(rightSpriteSize.x, rightSpriteSize.y, 1);
        }
        else if (movement.x < 0) // Moving left
        {
            spriteRenderer.sprite = leftSprite;
            spriteRenderer.transform.localScale = new Vector3(leftSpriteSize.x, leftSpriteSize.y, 1);
        }
    }

    public void EnableMovement(bool canMove)
    {
        CanMove = canMove;
    }
}
