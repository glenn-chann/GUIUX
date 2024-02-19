using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Animations;
using UnityEngine.Timeline;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    public float walkSpeed;
    public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public Animator animator;

    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isRunning;
    [HideInInspector] public bool isCrouching;
    [HideInInspector] public bool isProning;

    public GUIController guiController;

    public GameObject cameraPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        animator.SetFloat("X", horizontalInput);
        animator.SetFloat("Y", verticalInput);

        if(horizontalInput != 0 || verticalInput != 0)
        {
            isMoving = true;        
        }
        else
        {
            isMoving = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) && guiController.stamBar.rectTransform.sizeDelta.x > 0)
        {
            isRunning = true;
            if (isCrouching)
            {
                moveSpeed = sprintSpeed / 3;
            }
            else if (isProning)
            {
                moveSpeed = sprintSpeed / 5;
            }
            else
            {
                moveSpeed = sprintSpeed;
            }
        }
        else
        {
            isRunning = false;
            if (isCrouching)
            {
                moveSpeed = walkSpeed / 3;
            }
            else if (isProning)
            {
                moveSpeed = walkSpeed / 5;
            }
            else
            {
                moveSpeed = walkSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isProning)
        {
            isCrouching = !isCrouching;
            if (isCrouching)
            {
                cameraPos.transform.position = new Vector3(cameraPos.transform.position.x, cameraPos.transform.position.y - 4, cameraPos.transform.position.z);
            }
            else
            {
                cameraPos.transform.position = new Vector3(cameraPos.transform.position.x, cameraPos.transform.position.y + 4, cameraPos.transform.position.z);
            }
        
        }

        if (Input.GetKeyDown(KeyCode.C) && !isCrouching)
        {
            isProning = !isProning;
            if (isProning)
            {
                cameraPos.transform.position = new Vector3(cameraPos.transform.position.x, cameraPos.transform.position.y - 6, cameraPos.transform.position.z);
            }
            else
            {
                cameraPos.transform.position = new Vector3(cameraPos.transform.position.x, cameraPos.transform.position.y + 6, cameraPos.transform.position.z);
            }
        
        }

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isRunning", isRunning);
        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}