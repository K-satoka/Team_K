using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Attack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int attackDamage = 20;
    public float attackRate = 1f;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    private float nextAttackTime = 0f;

    public AudioSource AudioSource;
    public AudioClip AttackSE;

        void Update()
        {
            if (Time.time >= nextAttackTime)
            {
                if (Keyboard.current.zKey.wasPressedThisFrame)
                {
                    DoAttack();
                }
            }
        }

        void DoAttack()
        {
            nextAttackTime = Time.time + 1f / attackRate;

        if (AudioSource != null&&AttackSE!=null)
        {
            AudioSource.PlayOneShot(AttackSE);
        }

            // “–‚½‚è”»’è‚¾‚¯‚±‚±‚ÅŽÀŽ{
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>()?.TakeDamage(attackDamage);
            }
        }
}