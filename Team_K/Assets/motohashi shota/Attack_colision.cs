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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("“G‚ÉƒqƒbƒgI");
            // “G‚Éƒ_ƒ[ƒW‚ğ—^‚¦‚éˆ—‚ğ‚±‚±‚É‘‚­
            other.GetComponent<Enemy>()?.TakeDamage(10);
        }
    }
}
