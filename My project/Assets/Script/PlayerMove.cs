using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // Initial forward movement speed
    public float maxSpeed = 20f; // Maximum forward movement speed
    public float speedIncreaseRate = 0.1f; // Rate at which speed increases per second
    public float leftRightSpeed = 4f; // Lateral movement speed
    public float jumpForce = 7f; // Jump force
    public float extraGravity = 20f; // Extra gravity for faster descent
    public float slideDuration = 0.5f; // Slide duration

    private bool isJumping = false; // Tracks if the player is jumping
    private bool isSliding = false; // Tracks if the player is sliding

    private Rigidbody rb; // Reference to the Rigidbody
    private BoxCollider playerCollider; // Reference to the BoxCollider
    private Vector3 originalColliderSize; // Original size of the collider
    private Vector3 originalColliderCenter; // Original center of the collider

    private float groundCheckDistance = 1.1f; // Distance to check if the player is grounded

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();

        // Save original dimensions of the collider
        originalColliderSize = playerCollider.size;
        originalColliderCenter = playerCollider.center;

        // Lock rotations
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Start increasing the speed
        StartCoroutine(IncreaseSpeedOverTime());
    }

    void Update()
    {
        MoveForward();
        HandleInput();
    }

    void MoveForward()
    {
        // Constant forward movement
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void HandleInput()
    {
        // Handle jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded() && !isJumping)
        {
            Jump();
        }

        // Handle slide
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding && !isJumping)
        {
            StartCoroutine(Slide());
        }

        // Lateral movement
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > LevelBoundery.leftSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < LevelBoundery.rightSide)
            {
                transform.Translate(Vector3.right * Time.deltaTime * leftRightSpeed);
            }
        }
    }

    void Jump()
    {
        isJumping = true;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset vertical velocity
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply jump force
    }

    IEnumerator Slide()
    {
        isSliding = true;

        // Reduce collider size
        playerCollider.size = new Vector3(playerCollider.size.x, originalColliderSize.y / 2, playerCollider.size.z);
        playerCollider.center = new Vector3(playerCollider.center.x, originalColliderCenter.y / 2, playerCollider.center.z);

        yield return new WaitForSeconds(slideDuration);

        // Restore collider size
        playerCollider.size = originalColliderSize;
        playerCollider.center = originalColliderCenter;

        isSliding = false;
    }

    private void FixedUpdate()
    {
        // Extra gravity for controlled jump
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
        // Check if the player is grounded
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

    IEnumerator IncreaseSpeedOverTime()
    {
        Debug.Log($"Speed increased: {moveSpeed}");
        while (moveSpeed < maxSpeed)
        {
            yield return new WaitForSeconds(1f); // Adjust speed every second
            moveSpeed += speedIncreaseRate;

            // Clamp speed to maxSpeed to avoid exceeding the limit
            moveSpeed = Mathf.Min(moveSpeed, maxSpeed);

            Debug.Log($"Speed increased: {moveSpeed}");
        }
    }
}
