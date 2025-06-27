using UnityEngine;

public class CharacterStatistics : MonoBehaviour
{
    public string CharacterType { get; set; } = "Player"; // Default character type
    public int Health { get; set; }
    public int Damage { get; set; }
    public int Defense { get; set; }

    public int Level { get; set; } = 1; 
    public int Experience { get; set; }
    
    public void Stats(string type, int health, int damage, int defense, int level, int experience)
    {
        CharacterType = type;
        Health = health;
        Damage = damage;
        Defense = defense;
        Level = level;
        Experience = experience;
    }

    public void GetStats()
    {
        Debug.Log($"Health: {Health}, Damage: {Damage}, Defense: {Defense}, Level: {Level}, Experience: {Experience}");
    }

    public void TakeDamage()
    {
        Health -= Damage;
        if (Health < 0)
        {
            Health = 0; // Prevent health from going below zero
            Debug.Log("Character is dead.");
        }
        Debug.Log("Taking damage...");

    }

    public int DoDamage(int )
    {
        if (Health <= 0)
        {
            Debug.Log("Character is dead and cannot deal damage.");
            return 0; // Cannot deal damage if dead
        }
        else if (Health > 0 && CharacterType == "Enemy")
        {
            Debug.Log("Dealing damage...");
            return Damage; // Return the damage value
        }
        
    }
}
