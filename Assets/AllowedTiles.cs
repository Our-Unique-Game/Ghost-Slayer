using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * Ensures the player can only move on tiles that exist in the specified Tilemap(s).
 */
public class AllowedTiles : MonoBehaviour
{
    [SerializeField] private Tilemap[] allowedTilemaps; // Allowed Tilemaps

    /**
     * Checks if the given position is on a valid tile in the Tilemap(s).
     * @param position The grid position to check.
     * @return True if the tile exists, otherwise false.
     */
    public bool IsTileAllowed(Vector3Int position)
    {
        foreach (Tilemap tilemap in allowedTilemaps)
        {
            if (tilemap.HasTile(position)) // Check if tile exists in any allowed Tilemap
            {
                return true;
            }
        }
        return false; // No valid tile found
    }
}
