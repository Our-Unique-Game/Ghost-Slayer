using UnityEngine;
using TMPro; // For TextMeshPro UI elements
using UnityEngine.UI; // For Button UI elements
using System.Collections; // For Coroutine functionality

public class InstructionTrigger : MonoBehaviour
{
    [Header("Instruction Settings")]
    [SerializeField] private string instructionText = "Use arrow keys to move."; // The instruction to display
    [SerializeField] private float autoCloseTime = 5f; // Time after which the instruction auto-closes

    [Header("UI Elements")]
    [SerializeField] private GameObject instructionPanel; // Reference to the UI panel
    [SerializeField] private TMP_Text instructionTextUI; // Reference to the TextMeshPro Text element
    [SerializeField] private Button okButton; // "OK" button to dismiss the instructions

    private bool instructionShown = false; // Tracks if the instruction has already been shown
    private Movement playerMovement; // Reference to the player's movement script

    private Coroutine autoCloseCoroutine; // To handle auto-closing

    private void Awake()
    {
        // Find the player's Movement script
        playerMovement = FindObjectOfType<Movement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Show the instruction only if it hasn't been shown and the player enters
        if (collision.CompareTag("Player") && !instructionShown)
        {
            ShowInstruction();
        }
    }

    private void ShowInstruction()
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

        // Start the auto-close coroutine
        autoCloseCoroutine = StartCoroutine(AutoCloseInstruction());
    }

    private IEnumerator AutoCloseInstruction()
    {
        // Wait for the specified auto-close time
        yield return new WaitForSeconds(autoCloseTime);

        // Close the instruction automatically
        CloseInstruction();
    }

    private void CloseInstruction()
    {
        // If auto-close is running, stop it
        if (autoCloseCoroutine != null)
        {
            StopCoroutine(autoCloseCoroutine);
            autoCloseCoroutine = null;
        }

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
