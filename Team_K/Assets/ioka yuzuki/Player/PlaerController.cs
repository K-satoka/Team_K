using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaerController : MonoBehaviour
{
    Rigidbody2D rbody;  // Rigidbody2D型の変数
    float axisH = 0.0f; //入力
    public float speed = 805.0f;

    public float jump=9.0f;
    public LayerMask groundLayer;
    bool goJump =false;


    void Start()
    {
        // Rigidbody2Dをとってくる
        rbody=this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        
        //水平方向をチェックする
        axisH = Input.GetAxisRaw("Horizontal");
        if(axisH>0.0f)
        {
            //Debug.Log("右移動");
            transform.localScale = new Vector2(-1, 1);
        }
        else if(axisH<0.0f)
        {
            Debug.Log("左移動");
            transform.localScale=new Vector2(1, 1);
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
        bool onGround = Physics2D.CircleCast(transform.position,//発射位置
            0.2f,            //円の半径
            Vector2.down,    //発射方向
            0.0f,            //発射距離
            groundLayer);    //検出するレイヤー
        if (onGround || axisH != 0)
        {
            //地面の上or速度が０ではない
            //速度を更新する
            rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);
        }
        if (onGround) Debug.Log("tettse");
        if (onGround && goJump) {
            //地面の上でジャンプキーが押された
            //ジャンプさせる
            Vector2 jumpPw = new Vector2(0, jump);//ジャンプさせりベクトルを作る
           rbody.AddForce(jumpPw, ForceMode2D.Impulse);//
            goJump = false;
            Debug.Log("ジャンプ");
        }
       
    }
 //ジャンプ
 public void Jump()
    {
        goJump = true;//ジャンプフラグを立てる
    }
}
