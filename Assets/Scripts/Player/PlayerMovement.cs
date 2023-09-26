using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Player movement speed
    public float jumpForce = 5.0f; // Force applied when jumping
    public float airAcceleration = 0.4f; // Rate of air acceleration for horizontal movement
    public Transform groundCheck; // Transform representing a point just below the player to check for ground
    public Transform ceilingCheck; // Transform representing a point just below the player to check for ground
    public LayerMask groundLayer; // Layer mask to identify ground objects;
    public Transform playerCamera; // Reference to the player's camera
    public float rotationSpeed = 5.0f; // Speed at which the player rotates to match the camera's rotation

    private Rigidbody rb;
    private bool isGrounded;
    private bool isHittingCeiling;

    public int forwardInput;
    public int horizontalInput;

    public Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
        isHittingCeiling = Physics.CheckSphere(ceilingCheck.position, 01f, groundLayer);

        // Player inputs
        int upInput = Input.GetKey(KeyCode.W) ? 0 : 1;
        int leftInput = Input.GetKey(KeyCode.A) ? 0 : 1;
        int downInput = Input.GetKey(KeyCode.S) ? 0 : 1;
        int rightInput = Input.GetKey(KeyCode.D) ? 0 : 1;

        forwardInput = downInput - upInput;
        horizontalInput = leftInput - rightInput;

        // Calculate the movement direction relative to the camera's orientation
        Vector3 cameraForward = playerCamera.forward;
        Vector3 cameraRight = playerCamera.right;
        cameraForward.y = 0; // Keep the direction in the horizontal plane
        cameraForward.Normalize();

        // Calculate the amount to move in each direction relative to the camera
        moveDirection = (cameraForward * forwardInput + cameraRight * horizontalInput).normalized;

        // Calculate the amount you'll move in this frame
        Vector3 horizontalMoveVelocity = moveDirection * moveSpeed;
        rb.velocity = new Vector3(horizontalMoveVelocity.x, rb.velocity.y, horizontalMoveVelocity.z);

        // Player jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Retain horizontal velocity, only apply vertical jump force
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // If hitting the ceiling, set the player's velocity to 0 (so they don't just hang in the ceiling)
        if (isHittingCeiling && rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }

        // Rotate the player to match the camera's yaw
        RotatePlayerToCameraYaw();
    }

    private void RotatePlayerToCameraYaw()
    {
        Vector3 cameraEuler = playerCamera.eulerAngles;
        Vector3 newPlayerRotation = transform.eulerAngles;
        newPlayerRotation.y = cameraEuler.y;

        // Smoothly interpolate the player's rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newPlayerRotation), rotationSpeed * Time.deltaTime);
    }
}
