using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class stage3_BossMove : MonoBehaviour
{
    [Header("�^�[�Q�b�g")]
    public Transform player;

    [Header("�ړ��p�����[�^")]
    public float moveSpeed = 3f;
    public float stopDistance = 2f;

    [Header("�ːi�ݒ�")]
    public float dashSpeed = 12f;
    public float dashTime = 0.3f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashDirection = 0f; //�ːi�����Œ�p
    private bool isPreparingDash = false; // �ːi�ҋ@���t���O

    [Header("EndDash ����")]
    public float endDashBackSpeed = 12f;
    public float endDashBackTime = 0.6f;
    private bool isBusy = false;
    private bool isEndDashBack = false;
    private float endDashBackTimer = 0f;
    private float endDashBackDirection;

    [Header("空振り後退")]
    public float missBackSpeed = 6f;
    public float missBackTime = 0.3f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private BoxCollider2D boxCollider;//�R���C�_�[���]�p

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
            rb.linearVelocity = new Vector2(endDashBackDirection * endDashBackSpeed, rb.linearVelocity.y);
            if (endDashBackTimer >= endDashBackTime)
            {
                isEndDashBack = false;
                isBusy = false;
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
            return; // 後退中はそれ以外の処理を完全スキップ
        }    
        // ★突進中
        if (isDashing)
        { 
            dashTimer += Time.fixedDeltaTime;
            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, rb.linearVelocity.y);

            if (dashTimer >= dashTime)
                EndDash(DashEndType.Missed); // 突進終了 → 後退開始

            return; // 突進中はそれ以外の処理を完全スキップ
        }

        if (isBusy) return;

        // ★通常移動
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
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetBool("isMoving", false);

            dashDirection = player.position.x > transform.position.x ? 1f : -1f;
            StartCoroutine(StartDash());

            if (audioSource != null && Boss3SE != null)
                audioSource.PlayOneShot(Boss3SE);
        }
        else
        {
            float dir = Mathf.Sign(player.position.x - transform.position.x);
            rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
            anim.SetBool("isMoving", true);
        }
    }

    enum DashEndType
    {
        HitPlayer,   // プレイヤー命中
        Missed       // 空振り
    }

    DashEndType dashEndType;

    // -----------------------------
    // �� �ːi�J�n
    // -----------------------------
    //void StartDash()
    //{
    //    if (isDashing) return;
    //
    //    isDashing = true;
    //    dashTimer = 0f;
    //
    //    anim.SetBool("isDashing", true);
    //    anim.SetBool("isMoving", false);  // Idle �ɖ߂�������ɏ���
    //}


    //突E��前時間搭載用

    //�ːi�O���ԓ��ڗp�R�[�h
    IEnumerator StartDash()
    {
        isPreparingDash = true;   // ��������ҋ@���
        isBusy = true;

        yield return new WaitForSeconds(0.5f);

        isDashing = true;
        dashTimer = 0f;

        anim.SetBool("isDashing", true);
        anim.SetBool("isMoving", false);

        isPreparingDash = false;  // �ːi�J�n�őҋ@����
        isBusy = false;
    }
    // -----------------------------
    // �� �ːi�I��
    // -----------------------------
    void EndDash(DashEndType type)
    {
        isDashing = false;

        dashEndType = type;
        // 突進と逆方向
        endDashBackDirection = -dashDirection;

        isEndDashBack = true;
        endDashBackTimer = 0f;
        isBusy = true;

        if (type == DashEndType.Missed)
        {
            endDashBackSpeed = missBackSpeed;
            endDashBackTime = missBackTime;
        }

        anim.SetBool("isDashing", false);
    }
    void CancelDash()
    {
        isDashing = false;
        dashTimer = 0f;

        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        anim.SetBool("isDashing", false);
        anim.SetBool("isMoving", false);

        isBusy = false;
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDashing) return;

        if (collision.gameObject.CompareTag("Wall"))
        {
            // 壁命中 → 通常挙動へ戻す
            CancelDash();
        }
    }
}