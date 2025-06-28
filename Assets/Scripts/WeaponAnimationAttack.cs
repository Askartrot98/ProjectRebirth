using UnityEngine;

public class WeaponAnimationAttack : MonoBehaviour
{
    public PlayerAttack pA; // Assegna il riferimento al player nell’Inspector

    // Questo metodo viene chiamato dall'Animation Event
    public void CallPlayerDamage()
    {
        if (pA != null)
        {
            pA.DoDamage();
        }
    }
}
