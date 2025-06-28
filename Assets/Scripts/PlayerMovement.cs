using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Camera mainCamera;
    private Vector3 moveInput;
    private Rigidbody rb;



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
            Vector2 input = context.ReadValue<Vector2>();
            moveInput = new Vector3(input.x, 0, input.y); // X e Z


        }
        else if (context.canceled)
        {
            moveInput = Vector3.zero;
        }

    }

   


}
