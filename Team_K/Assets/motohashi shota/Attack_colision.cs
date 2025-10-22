using UnityEngine;

public class Attack_colision : MonoBehaviour
{
    public Collider2D attackCollider;

    // �U������ON
    public void EnableAttack()
    {
        attackCollider.enabled = true;
    }

    // �U������OFF
    public void DisableAttack()
    {
        attackCollider.enabled = false;
    }
}
