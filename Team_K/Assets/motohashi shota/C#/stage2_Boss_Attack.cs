using UnityEngine;
using System.Collections;

public class stage2_Boss_Attack : MonoBehaviour
{
    [Header("攻撃プレハブ")]
    public GameObject fistPrefab;
    public GameObject laserPrefab;

    [Header("攻撃間隔")]
    public float attackInterval = 2.5f;

    [Header("こぶしパラメータ")]
    public float fistFallSpeed = 60.0f;

    [Header("レーザーパラメータ")]
    public float laserDuration = 2.0f;
    public float laserAimTime = 1.0f; // 照準合わせ時間

    [Header("ターゲット設定")]
    public Transform player; // プレイヤー
    public Transform laserSpawnPoint; // ボスの口などの位置

    private float attackTimer;
    private int lastAttack = -1;

    void Start()
    {
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
       SpawnFistAtPlayer();
    }

    // ===== こぶし攻撃 =====
    void SpawnFistAtPlayer()
    {
        Vector3 spawnPos = new Vector3(player.position.x, Camera.main.orthographicSize + 1f, 0);
        GameObject fist = Instantiate(fistPrefab, spawnPos, Quaternion.identity);
        StartCoroutine(FallDown(fist.transform));
    }

    IEnumerator FallDown(Transform fist)
    {
        while (fist != null && fist.position.y > -Camera.main.orthographicSize - 2f)
        {
            fist.position += Vector3.down * fistFallSpeed * Time.deltaTime;
            yield return null;
        }
        if (fist != null) Destroy(fist.gameObject);
    }
}