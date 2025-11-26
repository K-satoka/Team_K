using UnityEngine;

public class BossAttackCollision : MonoBehaviour
{
    private Collider2D[] childColliders; // 子の当たり判定を保持
    private Collider2D selfCollider;     // 親自身の当たり判定を保持

    [Header("攻撃パラメータ")]
    [SerializeField] private int baseAttack = 15;  // 基礎攻撃力（エディタで変更可）
    private int currentAttack;                     // 現在の攻撃力

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

        // ボスはPlayerDataを参照しない
        currentAttack = baseAttack;
    }

    /// <summary>
    /// 子の当たり判定をONにする（攻撃時に呼ぶ）
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
    /// 子の当たり判定をOFFにする（攻撃後に呼ぶ）
    /// </summary>
    public void DisableAttack()
    {
        foreach (var col in childColliders)
        {
            if (col != selfCollider)
                col.enabled = false;
        }
    }

    /// <summary>
    /// 攻撃が当たった時の処理
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"ボス攻撃ヒット！攻撃力 {currentAttack}");
            //other.GetComponent<Player_Current_Hp>()?.TakeDamage(currentAttack);
        }
    }

    /// <summary>
    /// 攻撃力を上げる（スキルや難易度などで強化）
    /// </summary>
    public void IncreaseAttack(int value)
    {
        currentAttack += value;
        Debug.Log($"ボス攻撃力上昇！今は {currentAttack}");
    }

    /// <summary>
    /// 攻撃力を直接セットする（AIから呼び出し可）
    /// </summary>
    public void SetAttackPower(int value)
    {
        currentAttack = value;
    }

    /// <summary>
    /// 現在の攻撃力を取得
    /// </summary>
    public int GetAttackPower()
    {
        return currentAttack;
    }
}
