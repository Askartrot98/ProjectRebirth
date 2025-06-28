using UnityEngine;

public class WeaponAnimationAttack : MonoBehaviour
{
    public CharacterStatistics characterStatistics; // Assegna il riferimento al player nell’Inspector

    // Questo metodo viene chiamato dall'Animation Event
    public void CallPlayerDamage()
    {
        if (characterStatistics != null)
        {
            characterStatistics.DoDamage();
        }
    }
}
