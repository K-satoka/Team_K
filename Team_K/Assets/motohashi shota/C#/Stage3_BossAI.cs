using UnityEngine;
using System.Collections;

public class Stage3_BossAI : MonoBehaviour
{
    [Header("ターゲット")]
    public Transform player;

    [Header("移動パラメータ")]
    public float moveSpeed = 3f;
    public float stopDistance = 2f;

    [Header("攻撃設定")]
    public GameObject CQCPrefab;
    public GameObject SnowBallPrefab;
    public float attackInterval = 2.5f;

    [Header("CQCパラメータ")]
    public float CQCMoveSpeed = 5f;
    public float attackRange = 1.2f;
    public float retreatDistance = 3f;
    public float attackDuration = 0.5f;

    [Header("雪玉パラメータ")]
    public float snowballSpeed = 10f;
    public float snowballLifetime = 3f;
    public float aimTime = 0.5f;

    private Rigidbody2D rb;
    private Animator anim;

    private float attackTimer;
    private int lastAttack = -1;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // ★アニメーター取得

        attackTimer = Random.Range(0f, attackInterval);
    }

    void Update()
    {
        if (player == null) return;

        // アニメ用：攻撃中なら idle に切り替えておく
        anim.SetBool("isAttacking", isAttacking);

        // 攻撃中は移動しない
        if (isAttacking)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= stopDistance)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);

            attackTimer += Time.deltaTime;

            if (attackTimer >= attackInterval)
            {
                attackTimer = 0f;
                DoRandomAttack();
            }
        }
        else
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        rb.velocity = dir * moveSpeed;

        // アニメ：移動
        anim.SetBool("isMoving", true);
    }

    void DoRandomAttack()
    {
        int attackType;
        do { attackType = Random.Range(0, 2); } while (attackType == lastAttack);
        lastAttack = attackType;

        if (attackType == 0)
            StartCoroutine(CQC_Attack());
        else
            StartCoroutine(FireSnowBall());
    }

    IEnumerator CQC_Attack()
    {
        isAttacking = true;
        anim.SetBool("isMoving", false);
        anim.SetTrigger("CQC");   // ★アニメ再生

        while (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            rb.velocity = dir * CQCMoveSpeed;
            yield return null;
        }

        rb.velocity = Vector2.zero;

        if (CQCPrefab != null)
        {
            GameObject punch = Instantiate(CQCPrefab, player.position, Quaternion.identity);
            Destroy(punch, attackDuration);
        }

        yield return new WaitForSeconds(attackDuration);

        float moved = 0f;
        Vector3 retreatDir = (transform.position - player.position).normalized;

        while (moved < retreatDistance)
        {
            rb.velocity = retreatDir * CQCMoveSpeed;
            moved += CQCMoveSpeed * Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        isAttacking = false;
    }

    IEnumerator FireSnowBall()
    {
        isAttacking = true;
        anim.SetBool("isMoving", false);
        anim.SetTrigger("Snow");  // ★アニメ再生

        float timer = 0f;
        Vector3 dir = Vector3.right;

        while (timer < aimTime)
        {
            if (player != null)
                dir = (player.position - transform.position).normalized;
            timer += Time.deltaTime;
            yield return null;
        }

        GameObject snow = Instantiate(SnowBallPrefab, transform.position, Quaternion.identity);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        snow.transform.rotation = Quaternion.Euler(0, 0, angle);

        float life = 0f;
        while (life < snowballLifetime)
        {
            if (snow == null) break;
            snow.transform.position += dir * snowballSpeed * Time.deltaTime;
            life += Time.deltaTime;
            yield return null;
        }

        if (snow != null) Destroy(snow);

        isAttacking = false;
    }
}