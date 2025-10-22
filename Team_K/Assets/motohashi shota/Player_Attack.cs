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
            if (Keyboard.current.spaceKey.isPressed)  // �f�t�H���g�Ń}�E�X���N���b�N
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
    }
}