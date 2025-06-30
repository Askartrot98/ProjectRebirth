using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator animator;
    private CharacterStatistics cS;
    private PlayerHealth pH;
    private PlayerMovement pM;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float radius = 1.5f; // Radius for the attack hitbox
    private GameObject weaponHitbox; // Reference to the left hand hitbox
    public bool isAttacking = false; // Flag to check if the player is currently attacking
    public bool inputPressed = false; // Flag to check if the attack input is pressed


    [SerializeField] private Transform handBone; // Assegna il bone della mano (es: mixamorig:RightHand)
    [SerializeField] private GameObject weaponPrefab;

    private GameObject weaponInstance;

    void Start()
    {
        if (handBone != null && weaponPrefab != null)
        {
            weaponInstance = Instantiate(weaponPrefab, handBone);
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.transform.localRotation = Quaternion.identity;
            weaponHitbox = weaponInstance; // Assign the instantiated weapon to the hitbox
        }
    }


    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
        cS = GetComponent<CharacterStatistics>();
        pH = GetComponent<PlayerHealth>();
        pM = GetComponent<PlayerMovement>();

    }


    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Handle attack logic here
            Debug.Log("Attack performed");
            Attack(); // Call the Attack method to trigger the animation and logic
            pM.moveInput = Vector3.zero; // Ensure the player stops moving during the attack

        }

    }

    public void DoDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(weaponHitbox.transform.position, radius, layerMask);
        foreach (Collider enemy in hitColliders)
        {
            PlayerHealth enemyHealth = enemy.GetComponent<PlayerHealth>();
            CharacterStatistics csEnemy = enemy.GetComponent<CharacterStatistics>();
            if (enemyHealth != null && enemyHealth != this) // Ensure it's not self
            {
                enemyHealth.TakeDamage(cS.Damage);
                Debug.Log($"Dealt {cS.Damage} damage to {csEnemy.CharacterType} at position {weaponHitbox.transform.position}");
                StartCoroutine(HitStop(0.1f));
            }
        }



    }
    public void Attack()
    {
        isAttacking = true; // Set the attacking flag to true
        animator.SetTrigger("Attack"); // Trigger the attack animation
        Debug.Log("Attack animation triggered");
    }

    public void Attack2()
    {
        if (isAttacking && inputPressed) // Check if the player is currently attacking
        {
            animator.SetTrigger("Attack2"); // Trigger the second attack animation
            Debug.Log("Attack2 called, damage dealt");
        }
    }

    public void ResetAttack()
    {
        isAttacking = false; // Reset the attacking flag after the attack is done
        animator.ResetTrigger("Attack"); // Reset the attack trigger
    }

    public IEnumerator WaitForAttackEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        Attack2();
    }
    private void OnDrawGizmosSelected()
    {
        if (weaponHitbox == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weaponHitbox.transform.position, radius);


    }

    public IEnumerator HitStop(float duration)
    {
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 0.01f; // Rallenta il tempo quasi a zero
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = originalTimeScale;

    }
}

