using UnityEngine;

public class ExplodeMechanic : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] private GameObject explosionEffectPrefab; // Prefab for the explosion effect (white circle)
    [SerializeField] private float explosionRadius = 2f; // Radius of the explosion

    void Update()
    {
        // Trigger explosion when Space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Create explosion visual effect
        if (explosionEffectPrefab != null)
        {
            GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            explosionEffect.transform.localScale = new Vector3(explosionRadius * 2, explosionRadius * 2, 1); // Adjust size to match the radius
            Destroy(explosionEffect, 0.5f); // Destroy the effect after 0.5 seconds
        }

        // Detect all objects in the explosion radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Ghost"))
            {
                // Destroy the ghost immediately
                Destroy(hit.gameObject);

                // Notify GameManager
                FindObjectOfType<GameManager>().GhostDefeated();
                Debug.Log($"Ghost destroyed: {hit.name}");
            }
        }

        Debug.Log("Explosion triggered!");
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the explosion radius in the Scene view
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
