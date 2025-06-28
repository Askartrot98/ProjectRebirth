using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Camera mainCamera;
    private Vector3 moveInput;
    private Rigidbody rb;


    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashPower;

    private bool isDashing = false;
    private bool canDash = true;
    private Vector3 direction = Vector3.forward; // Default direction for dashing



    void Start()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component is missing from the GameObject.");
        }
        rb = GetComponent<Rigidbody>();



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 camForward = mainCamera.transform.forward;
        camForward.y = 0; // Keep the forward direction horizontal
        camForward.Normalize(); // Normalize to ensure consistent movement speed

        Vector3 camRight = mainCamera.transform.right;
        camRight.y = 0; // Keep the right direction horizontal
        camRight.Normalize(); // Normalize to ensure consistent movement speed


        Vector3 moveDirection = camForward * moveInput.z + camRight * moveInput.x;

        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f));
        }



    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isDashing) return;
            Vector2 input = context.ReadValue<Vector2>();
            moveInput = new Vector3(input.x, 0, input.y); // X e Z


        }
        else if (context.canceled)
        {
            moveInput = Vector3.zero;
        }

    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (rb != null)
            {
                Vector3 dashDirection = transform.forward; // Dash nella direzione in cui guarda il player
                StartCoroutine(Dash(dashDirection));
            }
        }
    }
    private bool IsGrounded()
    {
        // Raycast dal centro del player verso il basso, lungo la distanza del collider + un piccolo margine
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Jump input received");
            if (rb != null && IsGrounded())
            {
                Debug.Log("Player is grounded, jumping!");
                rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("Player is not grounded or Rigidbody missing");
            }
        }
    }

    public IEnumerator Dash(Vector3 direction) 
    {
        isDashing = true;
        canDash = false;
        rb.linearVelocity = direction * dashPower;
        yield return new WaitForSeconds(dashTime);
        rb.linearVelocity = Vector3.zero;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }
}
