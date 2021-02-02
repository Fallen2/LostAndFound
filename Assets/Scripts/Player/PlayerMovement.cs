using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float acceleration = 12.0f;

    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float gravity = -9.81f;

    private bool isGrounded;
    private float currentSpeed;

    Vector3 velocity;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //setting idle state
        if (x == 0 && z == 0)
        {
            animator.SetBool("Is Walking", false);
            animator.SetBool("Is Running", false);
            currentSpeed -= acceleration * Time.deltaTime;

            if(currentSpeed < 0.1f)
            {
                currentSpeed = 0f;
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Is Walking", false);
            animator.SetBool("Is Running", true);

            currentSpeed += acceleration * Time.deltaTime;
            if(currentSpeed > runSpeed)
            {
                currentSpeed = runSpeed;
            }
            controller.Move(move * currentSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Is Walking", true);
            animator.SetBool("Is Running", false);

            if (currentSpeed > walkSpeed + 0.1f && currentSpeed < walkSpeed - 0.1f)
            {
                currentSpeed = walkSpeed;
            }

            if (currentSpeed < walkSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
            else if(currentSpeed >= walkSpeed)
            {
                currentSpeed -= acceleration * Time.deltaTime;
            }

            controller.Move(move * currentSpeed * Time.deltaTime);
        }

        Debug.Log(currentSpeed);

        velocity.y += gravity * Time.deltaTime;
        Debug.Log(velocity.y);
        controller.Move(velocity * Time.deltaTime);
    }
}
