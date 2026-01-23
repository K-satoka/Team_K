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
            rb.linearVelocity = new Vector2(-dashDirection * endDashBackSpeed, rb.linearVelocity.y);

            if (endDashBackTimer >= endDashBackTime)
            {
                isEndDashBack = false;
                isBusy = false;
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
            return;
        }

        if (isBusy) return;

        // -----------------------------
        // �� �ːi���̏����i�ړ�AI�͖����j
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
            return; // �� �ړ�AI���~�߂�
        }

        // -----------------------------
        // �� ���̈ړ��R�[�h�i�����͂قڂ��̂܂܁j
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
            //�߂Â���������~�܂�
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetBool("isMoving", false);

            dashDirection = Mathf.Sign(player.position.x - transform.position.x);//�ːi�����Œ艻
            //�ːi�J�n
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
    void EndDash()
    {
        isDashing = false;

        // �� �����_�b�V���J�n
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

    //    // �ːi��~
    //    isDashing = false;
    //    anim.SetBool("isDashing", false);

    //    // �q�b�g�o�b�N�J�n
    //    isHitBack = true;
    //    hitBackTimer = 0f;

    //    // ���x���Z�b�g
    //    rb.velocity = Vector2.zero;

    //    // �ːi�����̋t�փm�b�N�o�b�N
    //    Vector2 knockbackForce = new Vector2(
    //        -dashDirection * hitBackPowerX,
    //        hitBackPowerY
    //    );

    //    rb.AddForce(knockbackForce, ForceMode2D.Impulse);
    //}
}