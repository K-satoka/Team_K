using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class stage3_BossMove : MonoBehaviour
{
    [Header("ƒ^[ƒQƒbƒg")]
    public Transform player;

    [Header("ˆÚ“®ƒpƒ‰ƒ[ƒ^")]
    public float moveSpeed = 3f;
    public float stopDistance = 2f;

    [Header("“Ëiİ’è")]
    public float dashSpeed = 12f;
    public float dashTime = 0.3f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashDirection = 0f; // © “Ëi•ûŒü‚ğŒÅ’è

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
        // š “Ëi’†‚Ìˆ—iˆÚ“®AI‚Í–³Œøj
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
            return; // © ˆÚ“®AI‚ğ~‚ß‚é
        }

        // -----------------------------
        // š Œ³‚ÌˆÚ“®ƒR[ƒhi‚±‚±‚Í‚Ù‚Ú‚»‚Ì‚Ü‚Üj
        // -----------------------------
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < stopDistance && !isDashing)
        {
            //‹ß‚Ã‚«‚·‚¬‚½‚ç~‚Ü‚é
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetBool("isMoving", false);

<<<<<<< HEAD
            float dirX = player.position.x - transform.position.x;

            // ãƒ—ãƒ¬ã‚¤ãƒ¤ãƒ¼ã®å·¦å³ã«å¿œã˜ã¦å‘ãã‚’å¤‰ãˆã‚‹
            if (dirX != 0)
            {
                sr.flipX = dirX > 0; // å³ãªã‚‰trueã€å·¦ãªã‚‰false
            }

            // â˜…åœæ­¢ã—ãŸç¬é–“ã«çªé€²é–‹å§‹
=======
            // š’â~‚µ‚½uŠÔ‚É“ËiŠJn
>>>>>>> e9f51578def6a057da087397100d7e1bfcd49ea5
            StartDash();

            if (audioSource != null && Boss3SE != null)
                audioSource.PlayOneShot(Boss3SE);
        }
        else
        {
            // ƒvƒŒƒCƒ„[‚ğ’Ç‚¢‚©‚¯‚éi‰¡ˆÚ“®‚Ì‚İj
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
    // š “ËiŠJn
    // -----------------------------
    void StartDash()
    {

        isDashing = true;
        dashTimer = 0f;

        dashDirection = Mathf.Sign(player.position.x - transform.position.x); // © “Ëi•ûŒüŒÅ’è
        anim.SetBool("isDashing", true);
        anim.SetBool("isMoving", false);  // Idle ‚É–ß‚éğŒ‚ğæ‚ÉÁ‚·
    }

    //çªé€²å‰æ™‚é–“æ­è¼‰ç”¨
    //IEnumerator StartDash()
    //{
    //    yield return new WaitForSeconds(1f);

    //    isDashing = true;
    //    dashTimer = 0f;

    //    dashDirection = Mathf.Sign(player.position.x - transform.position.x); // â† çªé€²æ–¹å‘å›ºå®š
    //    anim.SetBool("isDashing", true);
    //    anim.SetBool("isMoving", false);  // Idle ã«æˆ»ã‚‹æ¡ä»¶ã‚’å…ˆã«æ¶ˆã™
    //}
    // -----------------------------
    // š “ËiI—¹
    // -----------------------------
    void EndDash()
    {
        isDashing = false;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        anim.SetBool("isDashing", false); 
    }
}