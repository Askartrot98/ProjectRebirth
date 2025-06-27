using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Camera mainCamera;
    private Vector3 moveInput;



    void Start()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component is missing from the GameObject.");
        }


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camForward = mainCamera.transform.forward;
        camForward.y = 0; // Keep the forward direction horizontal
        camForward.Normalize(); // Normalize to ensure consistent movement speed

        Vector3 camRight = mainCamera.transform.right;
        camRight.y = 0; // Keep the right direction horizontal
        camRight.Normalize(); // Normalize to ensure consistent movement speed


        Vector3 moveDirection = camForward * moveInput.z + camRight * moveInput.x;
        
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        if(moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }



    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveInput = context.ReadValue<Vector3>();
           

        }
        else if (context.canceled)
        {
            moveInput = Vector3.zero;
        }

    }

   


}
