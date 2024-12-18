using UnityEngine;

// Makes a transform oscillate up and down relative to its start position
public class Oscillator : MonoBehaviour
{
    [SerializeField, Tooltip("Distance to oscillate (in meters)")]
    [SerializeField] private float m_Amplitude = 10.0f; // Increase this value to extend the oscillation range

    [SerializeField, Tooltip("Time to complete one oscillation (in seconds)")]
    [SerializeField] private float m_Period = 2.0f; // Time it takes for one full up-down oscillation

    [SerializeField] private Vector3 m_StartPosition;

    void Start()
    {
        // Record the starting position of the object
        m_StartPosition = transform.position;
    }

    void Update()
    {
        // Time-based oscillation
        float time = Time.time / m_Period; // Normalize time based on the period
        float offset = Mathf.Cos(2.0f * Mathf.PI * time); // Smooth oscillation between -1 and 1

        // Apply the movement along the Y-axis (up and down)
        Vector3 pos = m_StartPosition + Vector3.up * m_Amplitude * offset;

        // Update the transform position
        transform.position = pos;
    }
}