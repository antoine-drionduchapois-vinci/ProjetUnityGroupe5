using System.Collections;
using UnityEngine;

public class PlayerImmunity : MonoBehaviour
{
    public Material normalMaterial;    // Player's default material
    public Material safeMaterial;     // Material to indicate immunity
    public float immunityDuration = 5f; // Duration of the immunity effect
    private Renderer playerRenderer;
    private bool isImmune = false;     // Tracks immunity state

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
    }

    public void ActivateImmunity()
    {
        if (!isImmune)
        {
        
            StartCoroutine(GrantImmunity(immunityDuration));
        }
       
    }

    private IEnumerator GrantImmunity(float duration)
    {
    
        isImmune = true;

        // Change material to indicate immunity
        if (safeMaterial != null)
        {
            playerRenderer.material = safeMaterial;
        }
      

        int obstacleLayer = LayerMask.NameToLayer("Obstacle"); // Ensure the obstacle layer exists
        int playerLayer = gameObject.layer; // Get the player's layer
       
        Physics.IgnoreLayerCollision(playerLayer, obstacleLayer, true);


        yield return new WaitForSeconds(duration);
    

        Physics.IgnoreLayerCollision(playerLayer, obstacleLayer, false);
       



        // Restore normal state
        if (normalMaterial != null)
        {
            playerRenderer.material = normalMaterial;
        }
    
        isImmune = false;
      
    }
}
