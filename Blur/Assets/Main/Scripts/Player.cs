using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float acceleration = 10f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -30;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 3f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private Vector3 currentMove;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -5f;
        }

        // Movement input
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(x, 0f, z).normalized;
        Vector3 targetMove = transform.right * inputDirection.x + transform.forward * inputDirection.z;
        targetMove *= speed;

        currentMove = Vector3.Lerp(currentMove, targetMove, acceleration * Time.deltaTime);
        controller.Move(currentMove * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (velocity.y > 0 && !Input.GetButton("Jump"))
        {
            velocity.y += gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y += gravity * (fallMultiplier - 1) * Time.deltaTime;
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(Vector3.up * velocity.y * Time.deltaTime);
    }
}
