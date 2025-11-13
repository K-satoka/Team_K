using UnityEngine;
using System.Collections;

public class stage3_BossAttack : MonoBehaviour
{
    [Header("攻撃プレハブ")]
    public GameObject CQCPrefab;      // 近接攻撃エフェクトなど
    public GameObject SnowBallPrefab; // 雪玉攻撃用

    [Header("攻撃間隔")]
    public float attackInterval = 2.5f;

    [Header("CQCパラメータ")]
    public float moveSpeed = 5.0f;         // 近づく速度
    public float attackRange = 1.2f;       // 攻撃距離
    public float retreatDistance = 3.0f;   // 攻撃後に下がる距離
    public float attackDuration = 0.5f;    // 攻撃モーション時間

    [Header("雪玉パラメータ")]
    public float snowballSpeed = 10.0f;
    public float snowballLifetime = 3.0f;
    public float aimTime = 0.5f; // 照準時間

    [Header("ターゲット設定")]
    public Transform player;

    private float attackTimer;
    private int lastAttack = -1;
    private bool isAttacking = false;

    void Start()
    {
        attackTimer = Random.Range(0f, attackInterval);
    }

    void Update()
    {
        if (player == null || isAttacking) return;

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            attackTimer = 0f;
            DoRandomAttack();
        }
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

    // ======= 近接攻撃（プレイヤーに接近して攻撃） =======
    IEnumerator CQC_Attack()
    {
        isAttacking = true;

        // ① プレイヤーに接近
        while (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            yield return null;
        }

        // ② 攻撃モーション（拳エフェクトなど出す）
        if (CQCPrefab != null)
        {
            GameObject punch = Instantiate(CQCPrefab, player.position, Quaternion.identity);
            Destroy(punch, attackDuration);
        }

        yield return new WaitForSeconds(attackDuration);

        // ③ 後退（プレイヤーから少し離れる）
        Vector3 retreatDir = (transform.position - player.position).normalized;
        float retreatMoved = 0f;

        while (retreatMoved < retreatDistance)
        {
            transform.position += retreatDir * moveSpeed * Time.deltaTime;
            retreatMoved += moveSpeed * Time.deltaTime;
            yield return null;
        }

        isAttacking = false;
    }

    // ======= 雪玉攻撃 =======
    IEnumerator FireSnowBall()
    {
        isAttacking = true;

        // 照準フェーズ
        float timer = 0f;
        Vector3 dir = Vector3.right;
        while (timer < aimTime)
        {
            if (player != null)
                dir = (player.position - transform.position).normalized;
            timer += Time.deltaTime;
            yield return null;
        }

        // 発射
        GameObject snow = Instantiate(SnowBallPrefab, transform.position, Quaternion.identity);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        snow.transform.rotation = Quaternion.Euler(0, 0, angle);

        float life = 0f;
        while (life < snowballLifetime && snow != null)
        {
            snow.transform.position += dir * snowballSpeed * Time.deltaTime;
            life += Time.deltaTime;
            yield return null;
        }

        if (snow != null) Destroy(snow);

        isAttacking = false;
    }
}