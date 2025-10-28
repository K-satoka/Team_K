using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private Collider2D[] childColliders; // 子の当たり判定を保持
    private Collider2D selfCollider;     // 親自身の当たり判定を保持（残す用）

    private void Awake()
    {
        // 親自身の Collider2D を取得
        selfCollider = GetComponent<Collider2D>();

        // 子オブジェクトにある全ての Collider2D を取得（非アクティブも含む）
        childColliders = GetComponentsInChildren<Collider2D>(includeInactive: true);

        // 自分自身を除外して、初期状態では子の当たり判定をOFFに
        foreach (var col in childColliders)
        {
            if (col != selfCollider)
                col.enabled = false;
        }
    }

    /// <summary>
    /// 子の当たり判定をONにする
    /// </summary>
    public void EnableAttack()
    {
        foreach (var col in childColliders)
        {
            if (col != selfCollider)
                col.enabled = true;
        }
    }

    /// <summary>
    /// 子の当たり判定をOFFにする
    /// </summary>
    public void DisableAttack()
    {
        foreach (var col in childColliders)
        {
            if (col != selfCollider)
                col.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("敵にヒット！");
            other.GetComponent<EnemyHp>()?.TakeDamage(10);
        }
    }
}