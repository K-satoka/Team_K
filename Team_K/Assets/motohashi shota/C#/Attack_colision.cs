using System.Collections;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private Collider2D[] childColliders; // 子の当たり判定を保持
    private Collider2D selfCollider;     // 親自身の当たり判定を保持（残す用）

    [SerializeField] 
    private int baseAttack = 10;//基礎攻撃力
    private int currentAttack;//現在値(カードで強化される)

    
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

    currentAttack = baseAttack+PlayerData.Instance.attack_up;
    }

    private void Start()
    {
        
        currentAttack =baseAttack+ PlayerData.Instance.attack_up;
        Debug.Log("Start後の攻撃力: " + currentAttack);
    }



    /// <summary>
    /// 子の当たり判定をONにする
    /// </summary>
    public void EnableAttack(float duration=0.5f)
    {
        foreach (var col in childColliders)
        {
            if (col != selfCollider)
                col.enabled = true;
        }

        //数秒後に無効か　
        StartCoroutine(DisableAttackAfterSeconds(duration));
    }

    private IEnumerator DisableAttackAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DisableAttack();
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
            Debug.Log($"敵にヒット！攻撃力{currentAttack}");
            other.GetComponent<EnemyHp>()?.TakeDamage(currentAttack);
        }
    }

    ///<summary>
    ///攻撃力を上げる(カードで上がる)
    /// </summary>
  
    public void IncreaseAttack(int value)
    {
        currentAttack += value;

        PlayerData.Instance.attack_up += value;
        Debug.Log($"攻撃力上がったよ！今は{currentAttack} {value}");
    }
    ///<summary
    ///今の攻撃力
    ///</summary>
    
    public int GetAttackPower()
    {
        return currentAttack;
    }
    

}