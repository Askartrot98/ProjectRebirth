using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private CharacterStatistics cS;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cS = GetComponent<CharacterStatistics>();
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    
    public void TakeDamage(int damage)
    {
        cS.Health -= damage;
        cS.Health = Mathf.Clamp(cS.Health, 0, 100); // Ensure health does not exceed max health or drop below zero
        if (cS.Health <= 0)
        {
            Die();
        }
        Debug.Log($"{cS.CharacterType} is taking damage...");

    }
    private void Die()
    {
        Debug.Log($"{cS.CharacterType} has died.");
        // Handle death logic here, such as playing a death animation or disabling the player
        // For example:
        // animator.SetTrigger("Die");
        // gameObject.SetActive(false); // Disable the player object
    }
}
