using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaerController : MonoBehaviour
{
    Rigidbody2D rbody;  // Rigidbody2D型の変数
    float axisH = 0.0f; //入力
    public float speed = 805.0f;

    public float jump = 9.0f;
    public LayerMask GroundLayer;
    bool goJump = false;
    bool onGround = false;
    bool isAttacking = false;

    public int Max_JumpCount = 2;        //最大ジャンプ回数
    private int currentJumpCount = 0;

    public float knock_back_right;
    public float knock_back_left;

    //アニメーション対応
    Animator animator;//アニメーター
    public string waiting = "PlayerStop";
    public string PlayerMove = "PlayerMove";
    public string PlayerJump = "PlayerJump";
    public string PlayerAttack = "Player_Attack";
    string nowAnime = "";
    string oldAnime = "";

    void Start()
    {
        // Rigidbody2Dをとってくる
        rbody = this.GetComponent<Rigidbody2D>();//Rigidbody2Dからとってくる
        animator = GetComponent<Animator>();  //Animatorをとってくる
        nowAnime = waiting;                   //停止から開始
        oldAnime = waiting;                   //停止から開始
    }

    // Update is called once per frame
    void Update()
    {
        // 攻撃入力
        if (!isAttacking && Input.GetKeyDown(KeyCode.Z))
        {
            isAttacking = true;
            animator.Play(PlayerAttack);           // 攻撃アニメ再生
            StartCoroutine(EndAttackAnimation());  // 攻撃終了後に待機に戻す
        }

        //水平方向をチェックする
        axisH = Input.GetAxisRaw("Horizontal");
        if (axisH > 0.0f)
        {
            //右移動
            transform.localScale = new Vector2(-1, 1);
        }
        else if (axisH < 0.0f)
        {
            //左移動
            transform.localScale = new Vector2(1, 1);
        }

        //キャラクターをジャンプさせる
        if (Input.GetButtonDown("Jump"))
        {
            if (onGround || currentJumpCount < Max_JumpCount)
            {
                Jump(); //ジャンプ処理
            }
        }
    }

    // 攻撃アニメ終了後にフラグをリセットして待機に戻す
    IEnumerator EndAttackAnimation()
    {
        yield return new WaitForSeconds(0.25f); // 攻撃アニメの長さに合わせる
        isAttacking = false;
        nowAnime = waiting;
    }

    void FixedUpdate()
    {
        //地上判定
        onGround = Physics2D.CircleCast(transform.position,//発射位置
            0.2f,            //円の半径
            Vector2.down,    //発射方向
            0.0f,            //発射距離
            GroundLayer);    //検出するレイヤー

        // 移動処理
        if (onGround || axisH != 0)
        {
            //地面の上or速度が0ではない → 速度を更新
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }

        if (onGround)
        {
            currentJumpCount = 0; //ジャンプ回数リセット
        }

        //アニメーション更新
        if (!isAttacking) //攻撃中は他のアニメを上書きしない
        {
            if (onGround)
            {
                //地面の上：移動中か停止中かでアニメ切り替え
                nowAnime = axisH != 0 ? PlayerMove : waiting;
            }
            else
            {
                //空中
                nowAnime = PlayerJump;
            }

            //前回のアニメと異なる場合のみ再生
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
            if (transform.localScale.x >= 0)
            {
                //右向きノックバック
                Vector2 knockback = (transform.right * 1.3f + transform.up * 1.5f).normalized;
                this.rbody.AddForce(knockback * knock_back_left);
                Debug.Log("うえ");
            }
            else
            {
                //左向きノックバック
                Vector2 knockback2 = (transform.right * -1.3f + transform.up * 1.5f).normalized;
                this.rbody.AddForce(knockback2 * knock_back_right);
            }
        }
    }

    //ジャンプ処理
    public void Jump()
    {
        Vector2 jumpPw = new Vector2(0, jump);//ジャンプ用ベクトル
        rbody.AddForce(jumpPw, ForceMode2D.Impulse);
        goJump = false;
        currentJumpCount++;
    }
}