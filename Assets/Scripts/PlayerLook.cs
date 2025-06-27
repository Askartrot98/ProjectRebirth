using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private Camera mainCamera;

    private PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

 
    void Update()
    {
        
    }

    public void Look(InputAction.CallbackContext context)
    {
        if(context.performed)
        {

            Vector2 lookInput = context.ReadValue<Vector2>();
            float mouseX = lookInput.x * lookSpeed * Time.deltaTime;
            float mouseY = lookInput.y * lookSpeed * Time.deltaTime;
            // Rotate the player around the Y-axis
            transform.Rotate(Vector3.up, mouseX);
            // Rotate the camera around the X-axis
            mainCamera.transform.Rotate(Vector3.left, mouseY);
            mainCamera.transform.localEulerAngles = new Vector3(
                Mathf.Clamp(mainCamera.transform.localEulerAngles.x, -90f, 90f), // Clamp vertical rotation
                mainCamera.transform.localEulerAngles.y,
                mainCamera.transform.localEulerAngles.z
            );
        }
        else if (context.canceled)
        {
            // Optionally handle when the look input is canceled

        }
    }
}
