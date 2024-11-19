using System.Collections;
using UnityEngine;

public class PlayerImmunity : MonoBehaviour
{
     
    public float immunityDuration = 5f; // Duration of the immunity effect
    private Renderer playerRenderer;
    private bool isImmune = false;
    public ParticleSystem immunityEffect;

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
        if (immunityEffect != null)
        {
            immunityEffect.Play();
        }


        int obstacleLayer = LayerMask.NameToLayer("Obstacle"); // Ensure the obstacle layer exists
        int playerLayer = gameObject.layer; // Get the player's layer
       
        Physics.IgnoreLayerCollision(playerLayer, obstacleLayer, true);


        yield return new WaitForSeconds(duration);
    

        Physics.IgnoreLayerCollision(playerLayer, obstacleLayer, false);




        // Restore normal state
        if (immunityEffect != null)
        {
            immunityEffect.Stop();
        }


        isImmune = false;
      
    }
}
