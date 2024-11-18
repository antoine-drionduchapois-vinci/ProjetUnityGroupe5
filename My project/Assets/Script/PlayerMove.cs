using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement avant (ajustée)
    public float leftRightSpeed = 4f; // Vitesse latérale
    public float jumpForce = 7f; // Force du saut (ajustée pour un saut réaliste)
    public float extraGravity = 20f; // Gravité supplémentaire pour descendre plus rapidement
    public float slideDuration = 0.5f; // Durée de la glissade

    private bool isJumping = false; // Vérifie si le joueur saute
    private bool isSliding = false; // Vérifie si le joueur glisse

    private Rigidbody rb; // Référence au Rigidbody
    private BoxCollider playerCollider; // Référence au BoxCollider
    private Vector3 originalColliderSize; // Taille originale du Collider
    private Vector3 originalColliderCenter; // Centre original du Collider

    private float groundCheckDistance = 1.1f; // Distance pour vérifier si le joueur touche le sol

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();

        // Sauvegarde des dimensions originales du collider
        originalColliderSize = playerCollider.size;
        originalColliderCenter = playerCollider.center;

        // Verrouillage des rotations
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        MoveForward();
        HandleInput();
    }

    void MoveForward()
    {
        // Mouvement constant vers l'avant
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void HandleInput()
    {
        // Gestion du saut
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded() && !isJumping)
        {
            Jump();
        }

        // Gestion du slide
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding && !isJumping)
        {
            StartCoroutine(Slide());
        }

        // Déplacement latéral
        float horizontalInput = Input.GetAxis("Horizontal") * leftRightSpeed * Time.deltaTime;
        transform.Translate(Vector3.right * horizontalInput);
    }

    void Jump()
    {
        isJumping = true;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Réinitialise la vitesse verticale
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Applique la force du saut
    }

    IEnumerator Slide()
    {
        isSliding = true;

        // Réduction de la taille du collider
        playerCollider.size = new Vector3(playerCollider.size.x, originalColliderSize.y / 2, playerCollider.size.z);
        playerCollider.center = new Vector3(playerCollider.center.x, originalColliderCenter.y / 2, playerCollider.center.z);

        yield return new WaitForSeconds(slideDuration);

        // Rétablissement de la taille du collider
        playerCollider.size = originalColliderSize;
        playerCollider.center = originalColliderCenter;

        isSliding = false;
    }

    private void FixedUpdate()
    {
        // Gravité supplémentaire pour un saut plus contrôlé
        if (rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    bool IsGrounded()
    {
        // Vérifie si le joueur est en contact avec le sol
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
}
