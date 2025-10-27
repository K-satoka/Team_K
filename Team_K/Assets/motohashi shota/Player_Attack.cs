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
            if (Keyboard.current.zKey.isPressed)  // Z�L�[�ōU��
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // �U���A�j���[�V����
        animator.SetTrigger("Attack");
        // �U���͈͓��̓G�����o
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // ���o�����G�Ƀ_���[�W��^����
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>()?.TakeDamage(attackDamage);
            Debug.Log("�G�ɍU���q�b�g�I");
        }
    }
}