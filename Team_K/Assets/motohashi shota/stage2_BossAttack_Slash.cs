using UnityEngine;

public class stage2_BossAttack_Slash : MonoBehaviour
{
    [Header("Settings")]
    public float damage = 20f;
    public float range = 2f;
    public float duration = 0.3f;
    public LayerMask playerMask;
    public Animator animator; // optional

    public void DoAttack()
    {
        Debug.Log("Slash attack!");
        if (animator) animator.SetTrigger("Slash");

        // 攻撃タイミングを演出後に合わせたいならコルーチン化して遅延可能
        Invoke(nameof(DoDamage), 0.2f);
    }

    void DoDamage()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position + transform.right * range * 0.5f, range, playerMask);
        if (hit)
        {
            Debug.Log("Hit player with slash!");
            // PlayerHealth などがあれば：
            // hit.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.right * range * 0.5f, range);
    }
}
