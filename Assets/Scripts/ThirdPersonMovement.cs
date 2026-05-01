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


    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;

    float InputBuffer = 0f;
    [SerializeField] float JumpBufferDuration;
    bool JumpBuffer = false;
    public Transform camera;



    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;



    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

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


        currentDir = Vector2.SmoothDamp(currentDir, MoveValue, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * 2f * Time.deltaTime;

        Vector3 velocity = (camera.forward * currentDir.y + camera.right * currentDir.x) * Speed + Vector3.up * velocityY;


        controller.Move(velocity * Time.deltaTime);



        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, ground);

        

        if (JumpActive == 1)
        {

            JumpBuffer = true;
            InputBuffer = JumpBufferDuration;
        }


        if (isGrounded && JumpBuffer)
        {
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