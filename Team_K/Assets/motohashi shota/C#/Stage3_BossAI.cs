using UnityEngine;
using System.Collections;

public class Stage3_BossAI : MonoBehaviour
{
    [Header("�^�[�Q�b�g")]
    public Transform player;

    [Header("�ړ��p�����[�^")]
    public float moveSpeed = 3f;
    public float stopDistance = 2f;

    [Header("�U���ݒ�")]
    public GameObject CQCPrefab;
    public GameObject SnowBallPrefab;
    public float attackInterval = 2.5f;

    [Header("CQC�p�����[�^")]
    public float CQCMoveSpeed = 5f;
    public float attackRange = 1.2f;
    public float retreatDistance = 3f;
    public float attackDuration = 0.5f;

    [Header("��ʃp�����[�^")]
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
        anim = GetComponent<Animator>(); // ���A�j���[�^�[�擾

        attackTimer = Random.Range(0f, attackInterval);
    }

    void Update()
    {
        if (player == null) return;

        // �A�j���p�F�U�����Ȃ� idle �ɐ؂�ւ��Ă���
        anim.SetBool("isAttacking", isAttacking);

        // �U�����͈ړ����Ȃ�
        if (isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("isMoving", false);
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= stopDistance)
        {
            rb.linearVelocity = Vector2.zero;
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
        rb.linearVelocity = dir * moveSpeed;

        // �A�j���F�ړ�
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
        anim.SetTrigger("CQC");   // ���A�j���Đ�

        while (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            rb.linearVelocity = dir * CQCMoveSpeed;
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;

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
            rb.linearVelocity = retreatDir * CQCMoveSpeed;
            moved += CQCMoveSpeed * Time.deltaTime;
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        isAttacking = false;
    }

    IEnumerator FireSnowBall()
    {
        isAttacking = true;
        anim.SetBool("isMoving", false);
        anim.SetTrigger("Snow");  // ���A�j���Đ�

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