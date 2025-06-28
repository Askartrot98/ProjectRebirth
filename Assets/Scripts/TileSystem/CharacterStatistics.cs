using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CharacterStatistics : MonoBehaviour
{
    public string CharacterType = "Player"; // Default character type
    [SerializeField] private int health;
    [SerializeField] private int damage; 
    [SerializeField] private int defense; 
    [SerializeField] private int level; 
    [SerializeField] private int experience;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float radius = 1.5f; // Radius for the attack hitbox
    [SerializeField] private GameObject weaponHitbox; // Reference to the left hand hitbox



   

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0; // Prevent health from going below zero
            Debug.Log("Character is dead.");
        }
        Debug.Log("Taking damage...");

    }

    public void DoDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(weaponHitbox.transform.position, radius, layerMask);
        foreach (Collider enemy in hitColliders)
        {
            CharacterStatistics enemyStats = enemy.GetComponent<CharacterStatistics>();
            if (enemyStats != null && enemyStats != this) // Ensure it's not self
            {
                enemyStats.TakeDamage(damage);
                Debug.Log($"Dealt {damage} damage to {enemyStats.CharacterType} at position {weaponHitbox.transform.position}");
            }
        }



    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weaponHitbox.transform.position, radius);


    }

}
