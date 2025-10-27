using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private Collider2D attackCollider;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        if (attackCollider == null)
            Debug.LogError("このオブジェクトに Collider2D が見つかりません！");
        else
            attackCollider.enabled = false; // 初期はOFF
    }

    public void EnableAttack() => attackCollider.enabled = true;
    public void DisableAttack() => attackCollider.enabled = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("敵にヒット！");
            other.GetComponent<Enemy>()?.TakeDamage(10);
        }
    }
}