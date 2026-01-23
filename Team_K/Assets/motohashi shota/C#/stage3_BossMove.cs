using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class stage3_BossMove : MonoBehaviour
{
    [Header("ターゲット")]
    public Transform player;

    [Header("移動パラメータ")]
    public float moveSpeed = 3f;
    public float stopDistance = 2f;

    [Header("突進設定")]
    public float dashSpeed = 12f;
    public float dashTime = 0.3f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashDirection = 0f; //突進方向固定用
    private bool isPreparingDash = false; // 突進待機中フラグ

    [Header("EndDash 反動")]
    public float endDashBackSpeed = 12f;
    public float endDashBackTime = 0.6f;
    private bool isBusy = false;
    private bool isEndDashBack = false;
    private float endDashBackTimer = 0f;


    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private BoxCollider2D boxCollider;//コライダー反転用

    //SE
    public AudioSource audioSource;
    public AudioClip Boss3SE;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        if (isEndDashBack)
        {
            endDashBackTimer += Time.fixedDeltaTime;
            rb.velocity = new Vector2(-dashDirection * endDashBackSpeed, rb.velocity.y);

            if (endDashBackTimer >= endDashBackTime)
            {
                isEndDashBack = false;
                isBusy = false;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            return;
        }

        if (isBusy) return;

        // -----------------------------
        // ★ 突進中の処理（移動AIは無効）
        // -----------------------------
        if (isDashing)
        {
            dashTimer += Time.fixedDeltaTime;


            //float dirX = Mathf.Sign(player.position.x - transform.position.x);
            //rb.velocity = new Vector2(dirX * dashSpeed, rb.velocity.y);
            rb.velocity = new Vector2(dashDirection * dashSpeed, rb.velocity.y);

            if (dashTimer >= dashTime)
            {
                EndDash();
            }
            return; // ← 移動AIを止める
        }

        // -----------------------------
        // ★ 元の移動コード（ここはほぼそのまま）
        // -----------------------------
        float distance = Vector2.Distance(transform.position, player.position);
        float dirX = player.position.x - transform.position.x;

        if (dirX != 0)
        {
            bool facingRight = dirX > 0;
            sr.flipX = facingRight;
           FlipCollider(facingRight);
        }
        if (distance < stopDistance && !isDashing && !isBusy && !isPreparingDash)
        {
            //近づきすぎたら止まる
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("isMoving", false);

            dashDirection = Mathf.Sign(player.position.x - transform.position.x);//突進方向固定化
            //突進開始
            StartCoroutine(StartDash());

            if (audioSource != null && Boss3SE != null)
                audioSource.PlayOneShot(Boss3SE);
        }
        else
        {
            float dir = Mathf.Sign(player.position.x - transform.position.x);
            rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
            anim.SetBool("isMoving", true);
        }
    }

    // -----------------------------
    // ★ 突進開始
    // -----------------------------
    //void StartDash()
    //{
    //    if (isDashing) return;
    //
    //    isDashing = true;
    //    dashTimer = 0f;
    //
    //    anim.SetBool("isDashing", true);
    //    anim.SetBool("isMoving", false);  // Idle に戻る条件を先に消す
    //}


    //遯・ｲ蜑肴凾髢捺政霈臥畑

    //突進前時間搭載用コード
    IEnumerator StartDash()
    {
        isPreparingDash = true;   // ここから待機状態
        isBusy = true;

        yield return new WaitForSeconds(0.5f);

        isDashing = true;
        dashTimer = 0f;

        anim.SetBool("isDashing", true);
        anim.SetBool("isMoving", false);

        isPreparingDash = false;  // 突進開始で待機解除
        isBusy = false;
    }
    // -----------------------------
    // ★ 突進終了
    // -----------------------------
    void EndDash()
    {
        isDashing = false;

        // ★ 反動ダッシュ開始
        isEndDashBack = true;
        endDashBackTimer = 0f;
        isBusy = true;

        anim.SetBool("isDashing", false);
    }

    void FlipCollider(bool facingRight)
    {
        if (boxCollider != null)
        {
            Vector2 offset = boxCollider.offset;
            offset.x = Mathf.Abs(offset.x) * (facingRight ? 0.1f : -0.1f);
            boxCollider.offset = offset;
        }
    }

    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (!collision.gameObject.CompareTag("Player")) return;
    //    if (!isDashing) return;

    //    // 突進停止
    //    isDashing = false;
    //    anim.SetBool("isDashing", false);

    //    // ヒットバック開始
    //    isHitBack = true;
    //    hitBackTimer = 0f;

    //    // 速度リセット
    //    rb.velocity = Vector2.zero;

    //    // 突進方向の逆へノックバック
    //    Vector2 knockbackForce = new Vector2(
    //        -dashDirection * hitBackPowerX,
    //        hitBackPowerY
    //    );

    //    rb.AddForce(knockbackForce, ForceMode2D.Impulse);
    //}
}