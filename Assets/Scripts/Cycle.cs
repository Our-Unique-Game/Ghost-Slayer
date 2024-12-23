using UnityEngine;

/**
 * This component represents a cycle of points in space.
 * It can represent, for example, a patrol path for an AI.
 */
public class Cycle : MonoBehaviour
{
    [SerializeField] float gizmoRadius = 0.2f;  // for gizmos of patrol points

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Only draw gizmos if there are child points
        if (transform.childCount > 0)
        {
            // Loop through each child point and draw gizmos for visualization
            for (int i = 0; i < transform.childCount; ++i)
            {
                Transform currentPoint = transform.GetChild(i);

                // Draw each point as a sphere
                Gizmos.DrawSphere(currentPoint.position, gizmoRadius);

                // Connect the points in a loop to show the patrol path
                Transform nextPoint = transform.GetChild((i + 1) % transform.childCount); // The next point in the loop
                Gizmos.DrawLine(currentPoint.position, nextPoint.position);
            }
        }
    }
}
