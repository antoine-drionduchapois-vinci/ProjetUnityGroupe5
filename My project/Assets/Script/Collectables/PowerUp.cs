using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject player; // Assign the player in the Inspector
    public Material safeMaterial; // Material to indicate immunity
    public Material normalMaterial; // Player's normal material
    public float immunityDuration = 30f; // Duration of the immunity effect
    public LayerMask obstacleLayer; // Layer mask for obstacles

    private Renderer playerRenderer;

    void Start()
    {
        playerRenderer = player.GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            StartCoroutine(GrantImmunity());
            Destroy(gameObject); // Destroy the power-up object after activation
        }
    }

    IEnumerator GrantImmunity()
    {
        Debug.Log("Power-up activated: Player is immune to obstacles");

        // Change player material to indicate immunity
        playerRenderer.material = safeMaterial;

        // Ignore collisions between player and obstacles
        int obstacleLayerIndex = Mathf.RoundToInt(Mathf.Log(obstacleLayer.value, 2));
        Debug.Log($"Player Layer: {player.layer}, Obstacle Layer: {obstacleLayerIndex}");
        Physics.IgnoreLayerCollision(player.layer, obstacleLayerIndex, true);

        // Wait for immunity duration
        yield return new WaitForSeconds(immunityDuration);

        // Re-enable collisions and restore normal material
        Physics.IgnoreLayerCollision(player.layer, obstacleLayerIndex, false);
        playerRenderer.material = normalMaterial;

        Debug.Log("Power-up expired: Collisions re-enabled");
    }
}
