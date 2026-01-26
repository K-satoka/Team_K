using UnityEngine;
using System.Collections;

public class stage4_BossAttack1 : MonoBehaviour
{
    [Header("ターゲット")]
    public Transform player;
    [Header("遠距離攻撃プレハブ")]
    public GameObject SnowPrefab;
    public Transform SnowPoint;
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
    //個数カウント用
    private GameObject currentSnowball; // ★生成した雪玉を記録しておく
    [Header("アニメーション用")]
    bool isAttacking = false;

    [Header("近距離攻撃用")]
    public float meleeRange = 1.5f;       // 近接攻撃の距離
    public float meleeDuration = 0.5f;    // 攻撃判定が出ている時間
    public Collider2D meleeCollider;      // 攻撃用コライダー
    public int meleeDamage = 20;          // 攻撃力


    //SE
    public AudioSource audioSource;
    public AudioClip Boss4SE;
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

        //攻撃中に他のアニメーションに切り替わらないようにする処理
        if (isAttacking) return;

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

            float distance = Vector2.Distance(transform.position, player.position);

            // プレイヤーが近ければ近接攻撃、遠ければ雪玉
            // if (distance <= meleeRange)
            //近接攻撃開始（こっちも同じようにコルーチンで時間制御しながら動かす）
            //StartCoroutine(MeleeAttackRoutine());
            //else
            StartCoroutine(SnowAttackRoutine());

        }
    }


    // ======================================================
    //   遠距離攻撃（雪玉）アニメーションを最後まで再生＋発射
    // ======================================================
    IEnumerator SnowAttackRoutine()
    {
        if (currentSnowball != null)
        {
            Destroy(currentSnowball);
            currentSnowball = null;
            yield break;
        }

        isAttacking = true;

        animator.Play("ThrowSnow");
        yield return null;

        float animLength = animator.GetCurrentAnimatorStateInfo(0).length;

        // ★ エイム開始（生成はしない）
        StartCoroutine(FireSnow());

        if (audioSource != null && Boss4SE != null)
            audioSource.PlayOneShot(Boss4SE);

        yield return new WaitForSeconds(animLength);

        isAttacking = false;
    }

    //近距離バージョン
    IEnumerator MeleeAttackRoutine()
    {
        isAttacking = true;

        animator.Play("MeleeAttack");
        yield return null; // Animator 更新待ち

        float animLength = animator.GetCurrentAnimatorStateInfo(0).length;

        // 攻撃判定ON
        if (meleeCollider != null)
            meleeCollider.enabled = true;

        // 攻撃判定が出ている時間だけ待つ
        yield return new WaitForSeconds(meleeDuration);

        // 攻撃判定OFF
        if (meleeCollider != null)
            meleeCollider.enabled = false;

        // アニメ残りの部分を待つ
        yield return new WaitForSeconds(animLength - meleeDuration);

        isAttacking = false;
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
                attackDirection = (player.position - SnowPoint.position).normalized;

            // フレームごとに経過時間を加算
            timer += Time.deltaTime;
            // 次のフレームまで待つ（コルーチンの重要ポイント）
            yield return null;
        }
    }
    // ======================================================
    //     Animation Event から呼ばれる雪玉生成
    // ======================================================
    public void SpawnSnowFromAnim()
    {
        // 二重生成防止
        if (currentSnowball != null) return;

        // 念のため方向保険
        if (attackDirection == Vector2.zero && player != null)
            attackDirection = (player.position - SnowPoint.position).normalized;

        currentSnowball = Instantiate(SnowPrefab, SnowPoint.position, Quaternion.identity);
        currentSnowball.transform.localScale = new Vector3(3f, 3f, 3f);

        // 見た目向き
        SpriteRenderer snowSR = currentSnowball.GetComponent<SpriteRenderer>();
        if (snowSR != null)
            snowSR.flipX = attackDirection.x > 0;

        // 移動開始
        StartCoroutine(MoveSnow());
    }
    // ======================================================
    //   雪玉移動（元コードほぼそのまま）
    // ======================================================
    IEnumerator MoveSnow()
    {
        float life = 0f;

        while (life < 100f)
        {
            if (currentSnowball == null) yield break;

            currentSnowball.transform.position +=
                (Vector3)attackDirection * (spead * 2f) * Time.deltaTime;

            life += Time.deltaTime;
            yield return null;
        }

        if (currentSnowball != null)
        {
            Destroy(currentSnowball);
            currentSnowball = null;
        }
    }
}
