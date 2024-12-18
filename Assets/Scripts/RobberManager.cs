using UnityEngine;

public class RobberManager : MonoBehaviour
{
    [Header("Robber Settings")]
    [SerializeField] private GameObject robberPrefab; // The robber prefab to spawn
    [SerializeField] private Transform[] spawnPoints; // Array of spawn points
    [SerializeField] private float killDistance = 2f; // Distance required to kill a robber

    [SerializeField] private GameObject currentRobber; // Reference to the currently active robber
    [SerializeField] private int score = 0; // Player's score

    private void Start()
    {
        SpawnNewRobber();
    }

    private void Update()
    {
        if (currentRobber != null)
        {
            CheckForKill();
        }
    }

    private void SpawnNewRobber()
    {
        // Ensure there are spawn points and a prefab
        if (spawnPoints.Length == 0 || robberPrefab == null)
        {
            Debug.LogError("No spawn points or robber prefab assigned!");
            return;
        }

        // Select a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Spawn the robber at the selected spawn point
        currentRobber = Instantiate(robberPrefab, spawnPoint.position, Quaternion.identity);
    }

    private void CheckForKill()
    {
        // Check if the player is close enough to the current robber
        float distance = Vector2.Distance(transform.position, currentRobber.transform.position);
        if (distance <= killDistance && Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(currentRobber); // Destroy the current robber
            score++; // Increment the score
            Debug.Log("Robber killed! Score: " + score);

            // Spawn a new robber
            SpawnNewRobber();
        }
    }
}
