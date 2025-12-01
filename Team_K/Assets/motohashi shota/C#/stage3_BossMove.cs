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
    private float dashDirection = 0f; // ← 突進方向を固定

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    //SE
    public AudioSource audioSource;
    public AudioClip Boss3SE;

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


            float dirX = player.position.x - transform.position.x;

            // 繝励Ξ繧､繝､繝ｼ縺ｮ蟾ｦ蜿ｳ縺ｫ蠢懊§縺ｦ蜷代″繧貞､峨∴繧・
            if (dirX != 0)
            {
                sr.flipX = dirX > 0; // 蜿ｳ縺ｪ繧液rue縲∝ｷｦ縺ｪ繧映alse
            }


            // ★停止した瞬間に突進開始
            StartDash();

            if (audioSource != null && Boss3SE != null)
                audioSource.PlayOneShot(Boss3SE);
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


    //IEnumerator StartDash()
    //{
    //    yield return new WaitForSeconds(1f);

    //    isDashing = true;
    //    dashTimer = 0f;

    //    dashDirection = Mathf.Sign(player.position.x - transform.position.x); // 竊・遯・ｲ譁ｹ蜷大崋螳・
    //    anim.SetBool("isDashing", true);
    //    anim.SetBool("isMoving", false);  // Idle 縺ｫ謌ｻ繧区擅莉ｶ繧貞・縺ｫ豸医☆
    //}
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