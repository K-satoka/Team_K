using UnityEngine;

public class Attack_colision : MonoBehaviour
{
    public Collider2D attackCollider;

    // UŒ‚”»’èON
    public void EnableAttack()
    {
        attackCollider.enabled = true;
    }

    // UŒ‚”»’èOFF
    public void DisableAttack()
    {
        attackCollider.enabled = false;
    }
}
