using UnityEngine;
using System.Collections;

public class stage2_Boss_Attack : MonoBehaviour
{

    [Header("攻撃プレハブ")]
    public GameObject fistPrefab;
    public GameObject laserPrefab;

    [Header("攻撃間隔")]
    public float attackInterval = 2.5f; // 攻撃を行う頻度（共通）

    [Header("こぶしパラメータ")]
    public float fistFallSpeed = 5.0f;

    [Header("レーザーパラメータ")]
    public float laserSpeed = 10.0f;
    public float laserDuration = 2.0f;

    private float attackTimer;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        attackTimer = Random.Range(0f, attackInterval); // ランダム開始
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            attackTimer = 0f;
            DoRandomAttack();
        }
    }

    void DoRandomAttack()
    {
        // 0 = こぶし, 1 = レーザー
        int attackType = Random.Range(0, 2);

        if (attackType == 0)
        {
            SpawnFist();
        }
        else
        {
            StartCoroutine(FireLaser());
        }
    }

    void SpawnFist()
    {
        float screenWidth = cam.orthographicSize * cam.aspect;
        float x = Random.Range(-screenWidth, screenWidth);
        Vector3 spawnPos = new Vector3(x, cam.orthographicSize + 1f, 0);
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

        if (fist != null)
            Destroy(fist.gameObject);
    }

    IEnumerator FireLaser()
    {
        float screenWidth = cam.orthographicSize * cam.aspect;
        float y = Random.Range(-cam.orthographicSize * 0.5f, cam.orthographicSize * 0.8f);

        Vector3 spawnPos = new Vector3(transform.position.x, y, 0);
        GameObject laser = Instantiate(laserPrefab, spawnPos, Quaternion.identity);

        float timer = 0f;
        while (timer < laserDuration)
        {
            laser.transform.position += Vector3.right * laserSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(laser);
    }
}