using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatsSO", menuName = "Scriptable Objects/CharacterStatsSO")]
public class CharacterStatsSO : ScriptableObject
{
    public string characterType;
    public int health;
    public int damage;
    public int defense;
    public int level;
    public int experience;
}
