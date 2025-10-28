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
    public LayerMask GroundLayer;
    bool goJump =false;
    bool onGround = false;

    public int Max_JumpCount = 2;        //最大ジャンプ回数
    private int currentJumpCount = 0;


    //アニメーション対応
    Animator animator;//アニメーター
    public string waiting = "PlayerStop";
    public string PlayerMove = "PlayerMove";
    public string PlayerJump = "PlayerJump";
    string nowAnime = "";
    string oldAnime = "";


    void Start()
    {
        // Rigidbody2Dをとってくる
        rbody=this.GetComponent<Rigidbody2D>();//Rigidbody2Dからとってくる
        animator = GetComponent<Animator>();  //Animatorをとってくる
        nowAnime = waiting;                   //停止から開始
        oldAnime = waiting;                   //停止から開始
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
            //Debug.Log("左移動");
            transform.localScale=new Vector2(1, 1);
        }

        //キャラクターをジャンプさせる
        if(Input.GetButtonDown("Jump"))
        {
            if (onGround || currentJumpCount < Max_JumpCount)
            {
                Jump();
                Debug.Log("jannpu");
            }
           
        }
    }

    void FixedUpdate()
    {
        //地上判定
        bool onGround = Physics2D.CircleCast(transform.position,//発射位置
            0.2f,            //円の半径
            Vector2.down,    //発射方向
            0.0f,            //発射距離
            GroundLayer);    //検出するレイヤー

        

        if (onGround || axisH != 0)
        {
            //地面の上or速度が０ではない
            //速度を更新する
            rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);
        }
        if (onGround)
        {
            currentJumpCount = 0;
        }

        
        // if (onGround) Debug.Log("tettse");


        //アニメーションの更新
        if (onGround)
        {
            //地面の上
            if(axisH==0)
            {
                nowAnime = waiting;      //停止中
            }
            else
            {
                nowAnime = PlayerMove;  //移動
            }
        }
        else 
        {
            //空中
            nowAnime = PlayerJump;
        }   //ジャンプアニメーション完成時追加
        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);  //アニメーション追加
        }
    }
 //ジャンプ
 public void Jump()
    {
        Vector2 jumpPw = new Vector2(0, jump);//ジャンプさせりベクトルを作る
        rbody.AddForce(jumpPw, ForceMode2D.Impulse);//
        goJump = false;

        currentJumpCount++;
    }
}
