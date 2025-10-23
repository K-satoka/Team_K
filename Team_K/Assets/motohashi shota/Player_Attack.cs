using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int attackDamage = 20;
    public float attackRate = 1f;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    private float nextAttackTime = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Keyboard.current.zKey.isPressed)  // デフォルトでマウス左クリック
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // 攻撃アニメーション
        animator.SetTrigger("Attack");
        // 攻撃範囲内の敵を検出
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // 検出した敵にダメージを与える
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>()?.TakeDamage(attackDamage);
            Debug.Log("敵に攻撃ヒット！");
        }
    }
}