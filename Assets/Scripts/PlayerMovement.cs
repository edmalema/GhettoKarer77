using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorLock = true;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float Speed = 6.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] float BufferDuration = -0.2f;


    public float jumpHeight = 6f;
    float velocityY;
    bool isGrounded;

    float cameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    Vector3 velocity;


    float InputBuffer = 0f;
    bool JumpBuffer = false;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction LookAction;

    public bool AllowedToMove = true;

    public static PlayerMovement instance;

    [SerializeField] private bool Jump;




    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        LookAction = playerInput.actions["Look"];

        controller = GetComponent<CharacterController>();

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        if (AllowedToMove)
        {
            UpdateMouse();
            UpdateMove();

        }

    }

    public void StopMovement()
    {
        AllowedToMove = false;
    }

    public void StartMovement()
    {
        AllowedToMove = true;
    }
    void UpdateMouse()
    {
        Vector2 targetMouseDelta = LookAction.ReadValue<Vector2>();

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;

        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraCap;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMove()
    {



        Vector2 MoveValue = moveAction.ReadValue<Vector2>();
        float JumpActive = jumpAction.ReadValue<float>();


        currentDir = Vector2.SmoothDamp(currentDir, MoveValue, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * 2f * Time.deltaTime;



        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * Speed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);


        if (Jump)
        {



            if (JumpActive == 1)
            {

                JumpBuffer = true;
                InputBuffer = BufferDuration;
            }

            isGrounded = Physics.CheckSphere(groundCheck.position, 0.5f, ground);
            if (isGrounded && JumpBuffer)
            {

                velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);

            }

            InputBuffer -= Time.deltaTime;
            InputBuffer = Mathf.Clamp(InputBuffer, 0f, BufferDuration);

            if (InputBuffer == 0)
            {
                JumpBuffer = false;
            }

            if (isGrounded! && controller.velocity.y < -1f)
            {
                velocityY = -8f;
            }


        }




    }
}