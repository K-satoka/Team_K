using UnityEngine;
using System.Collections;

public class stage4_BossAttack : MonoBehaviour
{
    [Header("ターゲット")]
    public Transform player;
    [Header("遠距離攻撃プレハブ")]
    public GameObject SnowPrefab;
    [Header("スタート用")]
    private Animator animator;
    private Rigidbody2D rd;
    private SpriteRenderer sr;
    [Header("遠距離攻撃用")]
    //攻撃間隔
    public float attackInterval = 2.5f;
    //速度(雪球)
    public float spead = 5.0f;
    //エイム調整
    public float aimTime = 2f;
    //方向
    Vector2 attackDirection;
    //待機時間
    private float attackTimer;


    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        attackTimer = Random.Range(0f, attackInterval);
    }

    void Update()
    {
        // プレイヤーが存在しない場合は何もしない
        if (player == null) return;

        // -------------------------------
        // 攻撃タイマー（攻撃間隔用）を進める
        // Time.deltaTime は「前のフレームからの経過時間」
        // 1秒であれば 1.0、1/60秒であれば 0.016… が加算されていく
        // -------------------------------
        attackTimer += Time.deltaTime;

        // attackTimer が attackInterval（攻撃間隔）を超えたら攻撃
        if (attackTimer >= attackInterval)
        {
            // タイマーをリセット（次の攻撃に備える）
            attackTimer = 0f;

            // 雪玉攻撃を開始（コルーチンで時間制御しながら動かす）
            StartCoroutine(FireSnow());
        }
    }


    // ======================================================
    //                 遠距離攻撃（雪玉）
    // ======================================================
    IEnumerator FireSnow()
    {
        // -------------------------------
        // エイム（狙う）時間を計測するためのタイマー
        // aimTime は「最大何秒狙うか」という設定値
        // timer が 0 → aimTime まで増えることで、
        // その間プレイヤーの方向を追いかけ続ける
        // -------------------------------
        float timer = 0f;

        // aimTime 秒間だけプレイヤーの位置を追尾
        while (timer < aimTime)
        {
            // プレイヤーが存在するなら方向を更新
            // normalized は「向きだけ」を取り出す処理
            if (player != null)
                attackDirection = (player.position - transform.position).normalized;

            // フレームごとに経過時間を加算
            timer += Time.deltaTime;

            // 次のフレームまで待つ（コルーチンの重要ポイント）
            yield return null;
        }

        // -------------------------------
        // 雪玉プレハブをボスの位置に生成
        // Instantiate はゲームオブジェクトを複製する関数
        // -------------------------------
        GameObject snow = Instantiate(SnowPrefab, transform.position, Quaternion.identity);

        // -------------------------------
        // 雪玉の向きを攻撃方向に合わせる（見た目用）
        // Atan2 で角度を求め、Rad2Deg でラジアン → 度に変換
        // -------------------------------
        float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
        snow.transform.rotation = Quaternion.Euler(0, 0, angle);

        // -------------------------------
        // 雪玉が生存している時間を管理するタイマー
        // life が 3秒になるまで移動し続ける
        // -------------------------------
        float life = 0f;

        while (life < 3f)  // 雪玉寿命（3秒）
        {
            // 途中で雪玉が消えたらループを抜ける
            if (snow == null) break;

            // 雪玉を一定速度で進める
            // spead * Time.deltaTime で「1秒あたり spead の速度で動く」
            snow.transform.position += (Vector3)attackDirection * spead * Time.deltaTime;

            life += Time.deltaTime;

            // 次のフレームへ
            yield return null;
        }

        // 寿命が来たら雪玉を削除
        if (snow != null)
            Destroy(snow);
    }
}
