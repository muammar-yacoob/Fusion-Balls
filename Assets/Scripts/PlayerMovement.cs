using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float x, y;

    private float jumpForce = 10f;
    private float dashForce = 80;
    private float movementSpeed = 5;
    private float rotationSpeed = 100;

    private void Awake() => rb = GetComponent<Rigidbody>();

    private void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftControl))
            Dash();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + transform.forward * movementSpeed * y * Time.deltaTime);
        float turn = x * rotationSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    private void Jump()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Dash()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.forward * dashForce, ForceMode.Impulse);
    }
}