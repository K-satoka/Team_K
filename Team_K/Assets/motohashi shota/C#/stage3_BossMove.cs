using UnityEngine;

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
    private float dashDirection = 0f; // ← 突進方向を固定

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // -----------------------------
        // ★ 突進中の処理（移動AIは無効）
        // -----------------------------
        if (isDashing)
        {
            dashTimer += Time.fixedDeltaTime;

            //float dirX = Mathf.Sign(player.position.x - transform.position.x);
            //rb.velocity = new Vector2(dirX * dashSpeed, rb.velocity.y);
            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, rb.linearVelocity.y);

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

        if (distance < stopDistance && !isDashing)
        {
            //近づきすぎたら止まる
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetBool("isMoving", false);

            // ★停止した瞬間に突進開始
            StartDash();
        }
        else
        {
            // プレイヤーを追いかける（横移動のみ）
            float dirX = player.position.x - transform.position.x;

            if (dirX != 0)
            {
                sr.flipX = dirX > 0;
            }

            dirX = Mathf.Sign(dirX);

            rb.linearVelocity = new Vector2(dirX * moveSpeed, rb.linearVelocity.y);
            anim.SetBool("isMoving", true);
        }
    }

    // -----------------------------
    // ★ 突進開始
    // -----------------------------
    void StartDash()
    {
        isDashing = true;
        dashTimer = 0f;

        dashDirection = Mathf.Sign(player.position.x - transform.position.x); // ← 突進方向固定
        anim.SetBool("isDashing", true);
        anim.SetBool("isMoving", false);  // Idle に戻る条件を先に消す
    }

    // -----------------------------
    // ★ 突進終了
    // -----------------------------
    void EndDash()
    {
        isDashing = false;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        anim.SetBool("isDashing", false); 
    }
}