using UnityEngine;

/**
 * This component patrols continuously between given points without grid restrictions.
 */
public class Patroller : MonoBehaviour {
    [SerializeField] private Transform patrolPathParent;  // Parent object holding all patrol points
    [SerializeField] private float speed = 2f;            // Speed at which the patroller moves
    [SerializeField] private bool keepRotation = false;   // Option to allow or prevent rotation

    private Transform[] patrolPoints;   // Array to store patrol points
    private int currentPointIndex = 0;  // Index of the current patrol point

    private float startZ; // Store the original Z position

    private void Start() {
        // Store the initial Z position to keep it fixed
        startZ = transform.position.z;

        // Initialize the patrol points from the parent object
        if (patrolPathParent != null) {
            patrolPoints = new Transform[patrolPathParent.childCount];
            for (int i = 0; i < patrolPathParent.childCount; i++) {
                patrolPoints[i] = patrolPathParent.GetChild(i); // Get each child of the parent
            }
        }

        if (patrolPoints.Length > 0) {
            transform.position = patrolPoints[0].position; // Set initial position to the first patrol point
        }
    }

    private void Update() {
        if (patrolPoints.Length == 0) return;

        // Move directly towards the target point, but keep the original Z position
        Vector3 targetPosition = patrolPoints[currentPointIndex].position;
        targetPosition.z = startZ;  // Keep the original Z value constant
        MoveTowardTarget(targetPosition);

        // Check if the patroller has reached the target point
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f) {
            MoveToNextPoint();
        }
    }

    // Method to move the patroller directly toward the target position
    private void MoveTowardTarget(Vector3 target) {
        // Move directly towards the target, without affecting Z-axis
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (keepRotation) {
            RotateTowardsTarget(target);
        }
    }

    // Rotate smoothly towards the target position (optional)
    private void RotateTowardsTarget(Vector3 target) {
        Vector3 direction = (target - transform.position).normalized;
        if (direction != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }
    }

    // Move to the next patrol point, looping back to the start when reaching the end
    private void MoveToNextPoint() {
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
    }
}
