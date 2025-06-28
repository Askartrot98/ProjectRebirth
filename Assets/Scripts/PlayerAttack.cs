using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator animator;
    private CharacterStatistics cS;
    private PlayerHealth pH;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float radius = 1.5f; // Radius for the attack hitbox
    [SerializeField] private GameObject weaponHitbox; // Reference to the left hand hitbox


    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
        cS = GetComponent<CharacterStatistics>();
        pH = GetComponent<PlayerHealth>();

    }
    void Start()
    {

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

    private void OnDrawGizmosSelected()
    {

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

