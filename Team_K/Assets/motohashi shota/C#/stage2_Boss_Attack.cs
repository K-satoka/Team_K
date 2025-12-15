using UnityEngine;
using System.Collections;

public class stage2_Boss_Attack : MonoBehaviour
{
    [Header("攻撃プレハブ")]
    //攻撃のプレハブ（遠距離系のみ）
    public GameObject fistPrefab;
    public GameObject laserPrefab;

    [Header("攻撃間隔")]
    //落ちてくる間隔（０だととんでもない量降ってくる）
    public float attackInterval = 2.5f;

    [Header("こぶしパラメータ")]
    //落下速度
    public float fistFallSpeed = 6.0f;
    
    //レーザーの調整
    /*[Header("レーザーパラメータ")]
    public float laserSpeed = 12.0f;
    public float laserDuration = 2.0f;
    public float laserAimTime = 1.0f; // ← 撃つ前に照準を合わせる時間*/

    //ターゲット設定（基本的にプレイヤー）
    [Header("ターゲット設定")]
    public Transform player; // プレイヤーをInspectorで指定

    private float attackTimer;
    private int lastAttack = -1;
    private Camera cam;

    //SE
    public AudioSource audioSource;
    public AudioClip sundArm;
    public AudioClip sundArm2;


    void Start()
    {
        cam = Camera.main;
        //初撃までのインターバル（初期は2.5秒から０秒までからのランダム）
        attackTimer = Random.Range(0f, attackInterval);
    }

    void Update()
    {
        //プレイヤーのところに何もないと値を返す。
        if (player == null) return;

        //前回のフレームからの経過時間をattacktimerに足す
        attackTimer += Time.deltaTime;

        //attacktimerがインターバルを上回ったらattacktimerを０にして、攻撃用関数を呼び出す。
        if (attackTimer >= attackInterval)
        {
            attackTimer = 0f;
            DoRandomAttack();
        }
    }

    void DoRandomAttack()
    {
       // int attackType;
       // do { attackType = Random.Range(0, 2); } while (attackType == lastAttack);
       //  lastAttack = attackType;

       // if (attackType == 0)
       //こぶし攻撃用の関数
            SpawnFistAtPlayer();
       // else
       //   StartCoroutine(FireLaserWithAim());
    }

    // ===== こぶし攻撃 =====
    void SpawnFistAtPlayer()
    {
        //プレイヤートランスフォームで取得した、プレイヤーのX座標の真上の画面外にこぶしを生成
        Vector3 spawnPos = new Vector3(player.position.x, cam.orthographicSize + 1f, 0);
        //その位置にこぶしのプレハブを生成し、まっすぐ落とす。
        GameObject fist = Instantiate(fistPrefab, spawnPos, Quaternion.identity);

        if (audioSource != null&&sundArm!=null)
        {
            audioSource.PlayOneShot(sundArm);
            audioSource.PlayOneShot(sundArm2);
        }

        // ここで拳の大きさを変更してみる
        fist.transform.localScale = new Vector3(3f, 3f, 3f); // 好きな大きさに変更

        //フォールダウンの数値に合わせて落下
        StartCoroutine(FallDown(fist.transform));
    }

    IEnumerator FallDown(Transform fist)
    {
        //こぶしがあるかつこぶしのY座標が下の画面外に出たらコンルーチン（時間経過と一緒に処理する）を一時停止させる
        while (fist != null && fist.position.y > -cam.orthographicSize - 2f)
        {
            fist.position += Vector3.down * fistFallSpeed * Time.deltaTime;//毎フレームこぶしを下に落とす
            yield return null;//１フレーム待つ
        }
        //こぶしがあるならこぶしを消す。
        if (fist != null) Destroy(fist.gameObject);
    }

    // ===== レーザー攻撃（照準→固定発射） =====
    /*IEnumerator FireLaserWithAim()
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
    }*/
}