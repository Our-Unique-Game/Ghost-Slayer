using UnityEngine;

// Makes a transform oscillate up and down relative to its start position
public class Oscillator : MonoBehaviour
{
    [SerializeField, Tooltip("Distance to oscillate (in meters)")]
    private float m_Amplitude = 10.0f; // Amplitude of the oscillation (how far up/down)

    [SerializeField, Tooltip("Time to complete one oscillation (in seconds)")]
    private float m_Period = 2.0f; // Duration for one full oscillation cycle

    private Vector3 m_StartPosition; // Starting position to oscillate from

    private float m_StartTime; // Time at the start to control the oscillation's phase

    void Start()
    {
        // Record the starting position of the object
        m_StartPosition = transform.position;
        
        // Store the time at the start of the oscillation
        m_StartTime = Time.time;
    }

    void Update()
    {
        // Time-based oscillation: Calculate the elapsed time since the start
        float elapsedTime = Time.time - m_StartTime;

        // Oscillation calculation: Ensure it oscillates smoothly over time
        float oscillationValue = Mathf.Cos(2.0f * Mathf.PI * elapsedTime / m_Period); // Smooth oscillation between -1 and 1

        // Apply the oscillation along the Y-axis
        Vector3 newPosition = m_StartPosition + Vector3.up * m_Amplitude * oscillationValue;

        // Update the transform position, only adjusting the Y-axis
        transform.position = newPosition;
    }
}
