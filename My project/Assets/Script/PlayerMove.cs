using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de d�placement avant (ajust�e)
    public float leftRightSpeed = 4f; // Vitesse lat�rale
    public float jumpForce = 7f; // Force du saut (ajust�e pour un saut r�aliste)
    public float extraGravity = 20f; // Gravit� suppl�mentaire pour descendre plus rapidement
    public float slideDuration = 0.5f; // Dur�e de la glissade

    private bool isJumping = false; // V�rifie si le joueur saute
    private bool isSliding = false; // V�rifie si le joueur glisse

    private Rigidbody rb; // R�f�rence au Rigidbody
    private BoxCollider playerCollider; // R�f�rence au BoxCollider
    private Vector3 originalColliderSize; // Taille originale du Collider
    private Vector3 originalColliderCenter; // Centre original du Collider

    private float groundCheckDistance = 1.1f; // Distance pour v�rifier si le joueur touche le sol

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

       // transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);



        

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

        // D�placement lat�ral
        //   float horizontalInput = Input.GetAxis("Horizontal") * leftRightSpeed * Time.deltaTime;
        // transform.Translate(Vector3.right * horizontalInput);
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Allow movement to the boundary, not just before it

            if (this.gameObject.transform.position.x > LevelBoundery.leftSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Allow movement to the boundary, not just before it
            if (this.gameObject.transform.position.x < LevelBoundery.rightSide)
            {
                transform.Translate(Vector3.right * Time.deltaTime * leftRightSpeed);
            }
        }
    }

    void Jump()
    {
        isJumping = true;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // R�initialise la vitesse verticale
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Applique la force du saut
    }

    IEnumerator Slide()
    {
        isSliding = true;

        // R�duction de la taille du collider
        playerCollider.size = new Vector3(playerCollider.size.x, originalColliderSize.y / 2, playerCollider.size.z);
        playerCollider.center = new Vector3(playerCollider.center.x, originalColliderCenter.y / 2, playerCollider.center.z);

        yield return new WaitForSeconds(slideDuration);

        // R�tablissement de la taille du collider
        playerCollider.size = originalColliderSize;
        playerCollider.center = originalColliderCenter;

        isSliding = false;
    }

    private void FixedUpdate()
    {
        // Gravit� suppl�mentaire pour un saut plus contr�l�
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
        // V�rifie si le joueur est en contact avec le sol
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
}
