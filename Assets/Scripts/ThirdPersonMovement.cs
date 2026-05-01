using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{

    [SerializeField] float Speed = 6.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

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
    public bool DoubleJump = false;
    bool DoubleJumpActive = true;

    public Transform camera;

    public bool Dash = true;
    public float DashPower = 2f;
    private Vector3 DashAction;
    private bool Dashing = false;

    public bool AttackSlide = false;
    private Vector3 SlideSpeed;
    private float SpeedDecrease = 1f;



    [SerializeField] private Animator animator;

    //public Transform playerTransform;
    //public Transform PlayerTransform;
    //private Vector3 newRot;



    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction LookAction;



    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        LookAction = playerInput.actions["Look"];

        controller = GetComponent<CharacterController>();
    }



    void Update()
    {

        UpdateMove();
    }



    void UpdateMove()
    {
        Vector2 MoveValue = moveAction.ReadValue<Vector2>();
        float JumpActive = jumpAction.ReadValue<float>();


        //newRot = new Vector3(playerTransform.rotation.x, PlayerObjTransform.rotation.y, playerTransform.rotation.z);
        //playerTransform.rotation = Quaternion.Euler(newRot);
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * 2f * Time.deltaTime;

        Vector3 velocity = (camera.forward * currentDir.y + camera.right * currentDir.x) * Speed + Vector3.up * velocityY;

        SlideSpeed = velocity;
        SpeedDecrease = 1f;

        controller.Move(velocity * Time.deltaTime);

        float forwardValue = Mathf.Max(Mathf.Abs(Input.GetAxisRaw("Horizontal")), Mathf.Abs(Input.GetAxisRaw("Vertical")));

        animator.SetFloat("ForwardMovement", forwardValue);

        if (Dash)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DashAction = (camera.forward * currentDir.y + camera.right * currentDir.x) * Speed * DashPower + Vector3.up * velocityY;
                Dashing = true;
                StartCoroutine(DashFunc());
            }
            if (Dashing)
            {
                controller.Move(DashAction * Time.deltaTime);
            }
        }


        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, ground);


        bool AnimatorGrounded = Physics.CheckSphere(groundCheck.position, 0.6f, ground);
        animator.SetBool("IsGrounded", AnimatorGrounded);

        if (DoubleJump)
        {


            if (Input.GetButtonDown("Jump"))
            {
                if (!isGrounded && DoubleJumpActive)
                {
                    bool Jumping = true;
                    animator.SetBool("Jumping", Jumping);
                    Jumping = false;
                    animator.SetBool("Jumping", Jumping);



                    velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
                    DoubleJumpActive = false;
                    return;
                }


                JumpBuffer = true;
                InputBuffer = 0.3f;
            }



            if (isGrounded)
            {
                DoubleJumpActive = true;

            }
            if (isGrounded && JumpBuffer)
            {
                bool Jumping = true;
                animator.SetBool("Jumping", Jumping);
                Jumping = false;
                animator.SetBool("Jumping", Jumping);



                velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
                JumpBuffer = false;
                InputBuffer = 0f;
            }

            InputBuffer -= Time.deltaTime;
            InputBuffer = Mathf.Clamp(InputBuffer, 0f, 0.3f);

            if (InputBuffer == 0f)
            {
                JumpBuffer = false;
            }

            if (isGrounded! && controller.velocity.y < -1f)
            {
                velocityY = -8f;
            }


        }
        else
        {

            if (Input.GetButtonDown("Jump"))
            {

                JumpBuffer = true;
                InputBuffer = 0.5f;
            }


            if (isGrounded && JumpBuffer)
            {
                bool Jumping = true;
                animator.SetBool("Jumping", Jumping);
                Jumping = false;
                animator.SetBool("Jumping", Jumping);
                velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);

            }

            InputBuffer -= Time.deltaTime;
            InputBuffer = Mathf.Clamp(InputBuffer, 0f, 0.5f);

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


    IEnumerator DashFunc()
    {
        yield return new WaitForSeconds(0.1f);
        Dashing = false;

    }

}