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
        Rotate();
        
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            Land();

            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			direction = transform.TransformDirection(direction);
			direction *= moveSpeed;

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
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
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
