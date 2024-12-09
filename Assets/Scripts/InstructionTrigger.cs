using UnityEngine;
using TMPro; // For TextMeshPro UI elements
using UnityEngine.UI; // For Button UI elements

public class InstructionTrigger : MonoBehaviour
{
    [Header("Instruction Settings")]
    [SerializeField] private string instructionText = "Use arrow keys to move."; // The instruction to display

    [Header("UI Elements")]
    [SerializeField] private GameObject instructionPanel; // Reference to the UI panel
    [SerializeField] private TMP_Text instructionTextUI; // Reference to the TextMeshPro Text element
    [SerializeField] private Button okButton; // "OK" button to dismiss the instructions

    private bool instructionShown = false; // Tracks if the instruction has already been shown
    private PlayerMovement playerMovement; // Reference to the player movement script

    private void Awake()
    {
        // Find the player's movement script
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Show the instruction only if it hasn't been shown and the player enters
        if (collision.CompareTag("Player") && !instructionShown)
        {
            // Disable player movement
            if (playerMovement != null)
                playerMovement.CanMove = false;

            // Show the instruction panel
            instructionPanel.SetActive(true);

            // Set the instruction text
            instructionTextUI.text = instructionText;

            // Add a listener to the OK button
            okButton.onClick.AddListener(CloseInstruction);
        }
    }

    private void CloseInstruction()
    {
        // Mark the instruction as shown
        instructionShown = true;

        // Hide the instruction panel
        instructionPanel.SetActive(false);

        // Re-enable player movement
        if (playerMovement != null)
            playerMovement.CanMove = true;

        // Remove the listener to avoid duplicate calls
        okButton.onClick.RemoveListener(CloseInstruction);
    }
}
