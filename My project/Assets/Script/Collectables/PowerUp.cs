using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float immunityDuration = 5f; // Duration of the immunity effect
    public Material safeMaterial;      // Material to indicate immunity

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger immunity on the player
      
            PlayerImmunity playerImmunity = other.GetComponent<PlayerImmunity>();
            if (playerImmunity != null)
            {
             
                playerImmunity.ActivateImmunity();
            }

            // Destroy the power-up object
            Destroy(gameObject);
        }
    }
}
