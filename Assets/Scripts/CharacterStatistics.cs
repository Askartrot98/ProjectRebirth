using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CharacterStatistics : MonoBehaviour
{
    public string CharacterType = "Player"; // Default character type
    public int Health { get; set; }
    public int Damage { get; set; }
    public int Defense { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }

    [SerializeField] private CharacterStatsSO statsSO;

    private void Awake()
    {
        if (statsSO != null)
        {
            SetStats(
                statsSO.characterType,
                statsSO.health,
                statsSO.damage,
                statsSO.defense,
                statsSO.level,
                statsSO.experience
            );
        }
    }
    public void SetStats(string characterType, int health, int damage, int defense, int level, int experience)
    {
        CharacterType = characterType;
        Health = health;
        Damage = damage;
        Defense = defense;
        Level = level;
        Experience = experience;
    }

    public string GetStats()
    {
        return $"{CharacterType} \nHP: {Health}\nDefense: {Defense}\nAttack: {Damage}\nLevel: {Level}\nExperience: {Experience}";

    }
    

   

}
