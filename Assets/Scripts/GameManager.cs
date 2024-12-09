using UnityEngine;
using UnityEngine.UI; // For UI elements

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int totalGhosts = 10; // Total number of ghosts in the game
    [SerializeField] private GameObject winMessage; // Reference to the "You Win" UI message

    private int ghostsRemaining;

    void Start()
    {
        // Initialize ghost count and hide win message
        ghostsRemaining = totalGhosts;
        winMessage.SetActive(false);
    }

    public void GhostDefeated()
    {
        ghostsRemaining--;

        // Check if all ghosts are defeated
        if (ghostsRemaining <= 0)
        {
            DisplayWinMessage();
        }
    }

    private void DisplayWinMessage()
    {
        // Show the "You Win" message
        winMessage.SetActive(true);

        // Optionally, stop the player from moving
        FindFirstObjectByType<Movement>().CanMove = false;
    }
}
