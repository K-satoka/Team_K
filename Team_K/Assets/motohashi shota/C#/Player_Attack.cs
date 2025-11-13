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
            if (Keyboard.current.zKey.isPressed)  // ZƒL[‚ÅUŒ‚
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // UŒ‚ƒAƒjƒ[ƒVƒ‡ƒ“
        animator.SetTrigger("Attack");
        // UŒ‚”ÍˆÍ“à‚Ì“G‚ğŒŸo
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // ŒŸo‚µ‚½“G‚Éƒ_ƒ[ƒW‚ğ—^‚¦‚é
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>()?.TakeDamage(attackDamage);
            Debug.Log("“G‚ÉUŒ‚ƒqƒbƒgI");
        }
    }
    
}