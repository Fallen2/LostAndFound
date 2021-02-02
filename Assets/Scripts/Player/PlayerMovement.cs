using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public float acceleration = 1.0f;

    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float gravity = -9.81f;

    private float currentSpeed;

    Vector3 velocity;

    void Update()
    {
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
        controller.Move(velocity * Time.deltaTime);
    }
}
