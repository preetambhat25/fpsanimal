using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 6.0f;
    public float runSpeed = 12.0f;
    public float sensitivity = 2.0f;
    public float jumpHeight = 3.0f;

    private CharacterController characterController;
    private Camera playerCamera;

    private float rotationX = 0;
    private bool isGrounded;
    private float verticalVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        // Lock cursor and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovementInput();
        HandleMouseLook();
    }

    void HandleMovementInput()
    {
        // Check if the player is grounded
        isGrounded = characterController.isGrounded;

        // Player movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement;

        if (isGrounded)
        {
            // Walking
            movement = new Vector3(horizontal, 0, vertical) * speed;

            // Running
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement = new Vector3(horizontal, 0, vertical) * runSpeed;
            }

            // Jumping
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics.gravity.y));
            }
        }
        else
        {
            // In air, apply gravity
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
            movement = new Vector3(horizontal, verticalVelocity, vertical) * speed;
        }

        movement = transform.TransformDirection(movement);
        characterController.Move(movement * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        // Player rotation
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
    }
}
