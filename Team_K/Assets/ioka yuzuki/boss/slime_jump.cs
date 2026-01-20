using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class boss_jump: MonoBehaviour
{
    public float Enemy_jump = 0.0f;//スライムのジャンプ力
    private bool canjump = true;
    public Transform Ground;
    public LayerMask GroundLayer;
    bool goJump = false;
    private bool onGround;


    Rigidbody2D rb;  // Rigidbody2D型の変数

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();//Rigidbody2Dからとってくる

    }

    // Update is called once per frame
    void Update()
    {

        //地上判定
        bool onGround = Physics2D.CircleCast(transform.position,//発射位置
        0.8f,            //円の半径
        Vector2.down,    //発射方向
        0.0f,            //発射距離
        GroundLayer);    //検出するレイヤー

        if (onGround&&canjump)
        {
            
            Jump();
            canjump = false;
            StartCoroutine(ResetJumpDelay()); // 少し待ってから再ジャンプ許可
        }
    }
    void Jump()
    {

        Vector2 jumpPw = new Vector2(0, Enemy_jump);//ジャンプさせりベクトルを作る
        rb.AddForce(jumpPw, ForceMode2D.Impulse);
        Debug.Log("ddddddddddddddddddddddd");
    }
    // 再ジャンプを許可するための簡易クールタイム
    IEnumerator ResetJumpDelay()
    {
        yield return new WaitForSeconds(1.0f); // 1秒待つ（調整可）
        canjump = true;
    }
}
