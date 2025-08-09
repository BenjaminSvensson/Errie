using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [Header("Movement")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float rotationSpeed = 720f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -22f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 currentMove;
    private bool isGrounded;
    private bool canMove = true;

    public Vector3 LastMoveDirection { get; private set; } = Vector3.forward;

    void Awake()
    {
        Instance = this;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!canMove)
            return;

        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -5f;

        // Get input
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // Get camera-relative directions
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // Final move direction based on camera
        Vector3 inputDir = (camRight * x + camForward * z).normalized;

        Vector3 targetMove = inputDir * speed;
        currentMove = Vector3.Lerp(currentMove, targetMove, acceleration * Time.deltaTime);

        // Rotate if moving
        if (inputDir != Vector3.zero)
        {
            LastMoveDirection = inputDir;
            Quaternion targetRotation = Quaternion.LookRotation(inputDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move
        controller.Move(currentMove * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(Vector3.up * velocity.y * Time.deltaTime);
    }

    public void SetCanMove(bool state)
    {
        canMove = state;
    }

    public bool CanMove()
    {
        return canMove;
    }
}
