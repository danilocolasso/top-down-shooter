using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    const float gravity = 9.81f;

    private bool isGrounded;
    private Vector3 direction = Vector3.zero;
    private CharacterController controller;

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            Land();
            direction = transform.forward * moveSpeed * Input.GetAxis("Vertical");
            Rotate();

            if (Input.GetButton("Jump"))
            {
                Jump();
            }
        }

        ApplyGravity();
        
        controller.Move(direction * Time.deltaTime);
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotationSpeed);
    }

    void Land()
    {
        if (direction.y < 0)
        {
            direction.y = 0f;
        }
    }

    void Jump()
    {
        direction.y = jumpSpeed;
    }

    void ApplyGravity()
    {
        // Apply gravity. Gravity is multiplied by deltaTime twice.
        // This is because gravity should be applied as an acceleration (ms^-2)
        direction.y = direction.y - (gravity * Time.deltaTime);
    }
}
