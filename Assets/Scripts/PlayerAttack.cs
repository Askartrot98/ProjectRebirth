using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator animator; // Reference to the Animator component
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Handle attack logic here
            Debug.Log("Attack performed");
            animator.SetTrigger("Attack"); // Trigger the attack animation
            // You can add your attack logic here, such as playing an animation or dealing damage
        }
      
    }

    
}
