using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_move : MonoBehaviour
{
    Rigidbody2D rbody;//rigid2d型の変数
    float axisH = 0.0f;//入力
    public float speed = 1.0f;

    public float jump = 90.0f;//跳ぶ力
    public LayerMask groundLayer;//着地できるレイヤー
    bool goJump = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rigidbody2dをとってｋる
        rbody =this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //水平方向の入力をチェックする
        axisH = Input.GetAxisRaw("Horizontal");
        if (axisH > 0.0f)//向きの調整
        {
            transform.localScale = new Vector2(-1,1);//右移動
        }
        else if(axisH < 0.0f)
        {
            transform.localScale = new Vector2(1, 1);//左右反転
        }

        //キャラクターをジャンプさせる
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        //地上判定
        bool onGround = Physics2D.CircleCast(transform.position,
            0.2f,
            Vector2.down,
            0.0f,
            groundLayer);
        if (onGround || axisH != 0)
        {
            //地面の上または速度がゼロではない
            //速度を更新する
            rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);

        }
        if (onGround &&goJump)
        {
            //地面の上でジャンプキーが押された
            //ジャンプさせる
            Vector2 jumpPw = new Vector2(0, jump);//ジャンプさせるベクトルを作る
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);//瞬間的な力を加える
            goJump = false;//ジャンプフラグをおろす

        }

    }
    //ジャンプ
    public void Jump()
    {
        goJump = true;//ジャンプフラグを立てる
    }
}
