using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    private float verticalRotation = 0f;
    private Transform cameraTransform;
    private Rigidbody rb;
    public float moveSpeed = 5f;
    private float sprintSpeed;
    public bool isSprinting;
    private float moveHorizontal;
    private float moveForward;
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f; 
    public float ascendMultiplier = 2f;
    [SerializeField] private bool isGrounded = true;
    public LayerMask groundLayer;
    private float groundCheckTimer = 0f;
    private float groundCheckDelay = 0.3f;
    private float raycastDistance;
    public bool isMoving;
    [SerializeField] private PlayerStaminaHandler stamina;

    void Start()
    {
        sprintSpeed = moveSpeed * 2;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cameraTransform = Camera.main.transform;

        raycastDistance = .5f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isMoving = false;
        isSprinting = false;
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveForward = Input.GetAxisRaw("Vertical");

        RotateCamera();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (!isGrounded && groundCheckTimer <= 0f)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
            isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);
        }
        else
        {
            groundCheckTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKey(KeyCode.LeftShift) && stamina.currentStamina > 0 && isMoving)
        {
            moveSpeed = sprintSpeed;
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || stamina.currentStamina <= 0)
        {
            moveSpeed = sprintSpeed / 2;
            isSprinting = false;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
        ApplyJumpPhysics();
    }

    void MovePlayer()
    {
        isMoving = true;
        Vector3 movement = (transform.right * moveHorizontal + transform.forward * moveForward).normalized;
        Vector3 targetVelocity = movement * moveSpeed;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.linearVelocity = velocity;

        if (isGrounded && moveHorizontal == 0 && moveForward == 0)
        {
            isMoving = false;
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

    }

    void RotateCamera()
    {
        if (Time.timeScale != 0)
        {
            float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
            transform.Rotate(0, horizontalRotation, 0);

            verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -20f, 0f);

            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }

    void Jump()
    {
        isGrounded = false;
        groundCheckTimer = groundCheckDelay;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }

    void ApplyJumpPhysics()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * ascendMultiplier * Time.fixedDeltaTime;
        }
    }
}
