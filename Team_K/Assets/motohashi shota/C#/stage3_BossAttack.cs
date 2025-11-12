using UnityEngine;
using System.Collections;

public class stage3_BossAttack : MonoBehaviour
{
    [Header("攻撃プレハブ")]
    public GameObject fistPrefab;
    public GameObject laserPrefab;

    [Header("攻撃間隔")]
    public float attackInterval = 2.5f;

    [Header("こぶしパラメータ")]
    public float fistFallSpeed = 6.0f;

    [Header("レーザーパラメータ")]
    public float laserSpeed = 12.0f;
    public float laserDuration = 2.0f;
    public float laserAimTime = 1.0f; // ← 撃つ前に照準を合わせる時間

    [Header("ターゲット設定")]
    public Transform player; // プレイヤーをInspectorで指定

    private float attackTimer;
    private int lastAttack = -1;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        attackTimer = Random.Range(0f, attackInterval);
    }

    void Update()
    {
        if (player == null) return;

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
        SpawnFistAtPlayer();
         else
           StartCoroutine(FireLaserWithAim());
    }

    // ===== こぶし攻撃 =====
    void SpawnFistAtPlayer()
    {
        Vector3 spawnPos = new Vector3(player.position.x, cam.orthographicSize + 1f, 0);
        GameObject fist = Instantiate(fistPrefab, spawnPos, Quaternion.identity);
        StartCoroutine(FallDown(fist.transform));
    }

    IEnumerator FallDown(Transform fist)
    {
        while (fist != null && fist.position.y > -cam.orthographicSize - 2f)
        {
            fist.position += Vector3.down * fistFallSpeed * Time.deltaTime;
            yield return null;
        }
        if (fist != null) Destroy(fist.gameObject);
    }

    // ===== レーザー攻撃（照準→固定発射） =====
    IEnumerator FireLaserWithAim()
    {
        // --- ① 照準フェーズ ---
        float aimTimer = 0f;
        Vector3 dir = Vector3.right; // 初期方向
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);

        while (aimTimer < laserAimTime)
        {
            if (player != null)
                dir = (player.position - transform.position).normalized;

            // レーザーの向きをプレイヤー方向に更新
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            laser.transform.rotation = Quaternion.Euler(0, 0, angle);

            aimTimer += Time.deltaTime;
            yield return null;
        }

        // --- ② 発射フェーズ（照準固定） ---
        Vector3 fireDir = dir; // この時点の方向を固定
        float moveTimer = 0f;

        while (moveTimer < laserDuration)
        {
            laser.transform.position += fireDir * laserSpeed * Time.deltaTime;
            moveTimer += Time.deltaTime;
            yield return null;
        }

        Destroy(laser);
    }
}
