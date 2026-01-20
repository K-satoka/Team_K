using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;  // Rigidbody2D
    float axisH = 0.0f; //移動
    public float speed = 805.0f;

    public float jump = 9.0f;
    public LayerMask GroundLayer;
    bool goJump = false;
    bool onGround = false;
    bool isAttacking = false;

    public int Max_JumpCount = 2;
    private int currentJumpCount = 0;

    public float knock_back_right;
    public float knock_back_left;

    public float at_timer=0f;
    private float timer;

  //アニメーション--------------------------------
    Animator animator;
    public string waiting = "PlayerStop";
    public string PlayerMove = "PlayerMove";
    public string PlayerJump = "PlayerJump";
    public string PlayerAttack = "Player_Attack";
    string nowAnime = "";
    string oldAnime = "";

    void Start()
    {
        // Rigidbody2D
        rb = this.GetComponent<Rigidbody2D>();//Rigidbody2D
        animator = GetComponent<Animator>();  //Animator
        nowAnime = waiting;
        oldAnime = waiting;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking && Input.GetKeyDown(KeyCode.Z))
        {
            isAttacking = true;
            animator.Play(PlayerAttack);
            StartCoroutine(EndAttackAnimation());
        }

        axisH = Input.GetAxisRaw("Horizontal");
        if (axisH > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (onGround || currentJumpCount < Max_JumpCount)
            {
                Jump(); 
            }
        }
    }

    IEnumerator EndAttackAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
        nowAnime = waiting;
        animator.Play(waiting);
    }

    void FixedUpdate()
    {
        onGround = Physics2D.CircleCast(transform.position,
            0.2f,
            Vector2.down,
            0.0f,
            GroundLayer); 

        if (onGround || axisH != 0)
        {
            rb.linearVelocity = new Vector2(axisH * speed, rb.linearVelocity.y);
        }

        if (onGround)
        {
            currentJumpCount = 0;
        }

      
        if (!isAttacking) 
        {
            if (onGround)
            {
                nowAnime = axisH != 0 ? PlayerMove : waiting;
            }
            else
            {
                nowAnime = PlayerJump;
            }
            if (nowAnime != oldAnime)
            {
                oldAnime = nowAnime;
                animator.Play(nowAnime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // 攻撃元から自分への方向
            Vector2 knockDir = (transform.position - collision.transform.position).normalized;

            // 上方向も加える
            knockDir += Vector2.up * 0.5f;
            knockDir.Normalize();

            // 左右で別の力を設定
            float force = 1.5f;
            if (knockDir.x > 0)
            {
                // 左から攻撃された → 右に吹っ飛ぶ
                force = knock_back_right;
            }
            else
            {
                // 右から攻撃された → 左に吹っ飛ぶ
                force = knock_back_left;
            }
            rb.AddForce(knockDir * force);
        }
    }
    public void Jump()
    {
        Vector2 jumpPw = new Vector2(0, jump);
        rb.AddForce(jumpPw, ForceMode2D.Impulse);
        goJump = false;
        currentJumpCount++;
    }
}