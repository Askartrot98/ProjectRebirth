using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 10f; 
    [SerializeField] private Camera mainCamera;
    public Vector3 moveInput;
    private Rigidbody rb;


    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashPower;

    private bool isDashing = false;
    private bool canDash = true;
    private Vector3 direction = Vector3.forward; // Default direction for dashing
    private Animator anim;
    private PlayerAttack pA; // Reference to PlayerMovement for checking isAttacking
    private PlayerInput playerInput; // Reference to PlayerInput for input handling



    void Start()
    {
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        pA = GetComponent<PlayerAttack>();


        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component is missing from the GameObject.");
        }
        rb = GetComponent<Rigidbody>();



    }

    private void Update()
    {
        //if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        //{
        //    anim.applyRootMotion = true;
        //}
        //else
        //{
        //    anim.applyRootMotion = false;
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 camForward = mainCamera.transform.forward;
        //camForward.y = 0; // Keep the forward direction horizontal
        //camForward.Normalize(); // Normalize to ensure consistent movement speed

        //Vector3 camRight = mainCamera.transform.right;
        //camRight.y = 0; // Keep the right direction horizontal
        //camRight.Normalize(); // Normalize to ensure consistent movement speed


        //Vector3 moveDirection = camForward * moveInput.z + camRight * moveInput.x;

        //rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        //if (moveDirection != Vector3.zero)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        //    rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f));
        //}
        if (moveInput != Vector3.zero)
        {
            Vector3 camForward = mainCamera.transform.forward;
            camForward.y = 0;
            camForward.Normalize();
            Vector3 camRight = mainCamera.transform.right;
            camRight.y = 0;
            camRight.Normalize();
            Vector3 moveDirection = camForward * moveInput.z + camRight * moveInput.x;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f));
        }



    }

    public void Move(InputAction.CallbackContext context)
    {
        //if (context.performed)
        //{
        //    if (isDashing) return;
        //    if(pA.isAttacking) return; // Prevent movement while attacking
        //    Vector2 input = context.ReadValue<Vector2>();
        //    moveInput = new Vector3(input.x, 0, input.y); // X e Z
        //    float speed = moveInput.magnitude; // Calcola la velocità in base all'input
        //    anim.SetFloat("Speed", speed);


        //}
        //else if (context.canceled)
        //{
        //    moveInput = Vector3.zero;
        //    anim.SetFloat("Speed", 0f); // Imposta la velocità a 0 quando l'input è cancellato
        //}
        if (context.performed)
        {
            if (isDashing) return;
            if (pA.isAttacking) return; // Blocca il movimento durante l'attacco

            Vector2 input = context.ReadValue<Vector2>();
            moveInput = new Vector3(input.x, 0, input.y); // Solo per la rotazione

            // Calcola la magnitudo dell'input per il blend delle animazioni
            float speed = input.magnitude;
            anim.SetFloat("Speed", speed);
        }
        else if (context.canceled)
        {
            moveInput = Vector3.zero;
            anim.SetFloat("Speed", 0f);
        }

    }
    void OnAnimatorMove()
    {
        if (anim.applyRootMotion && rb != null)
        {
            rb.MovePosition(rb.position + anim.deltaPosition);
            rb.MoveRotation(anim.rootRotation);
        }
    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (context.started && canDash)
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
                anim.SetTrigger("Jump"); // Trigger the jump animation
            }
            else
            {
                Debug.Log("Player is not grounded or Rigidbody missing");
            }
        }
        else if (context.canceled)
        {
           anim.ResetTrigger("Jump"); // Reset the jump animation trigger when input is canceled
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
