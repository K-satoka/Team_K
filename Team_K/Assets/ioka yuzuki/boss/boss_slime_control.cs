using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

//ENEMY

[RequireComponent (typeof (Rigidbody2D))]
public class boss : MonoBehaviour
{
    Rigidbody2D rbody;  // Rigidbody2D型の変数
    public float Enemy_speed;    //スライムの速度
    public float Enemy_jump=0.0f;//スライムのジャンプ力
    public float jumpTriggerDistance = 5f;//プレイヤーとの距離
    private Transform Player;
    public Transform Ground;
    private string EnemyTag = "Enemy";

   // private bool onGround;



    public LayerMask GroundLayer;
    bool goJump = false;

    void Start()
    {
        // Rigidbody2Dをとってくる
        rbody = this.GetComponent<Rigidbody2D>();//Rigidbody2Dからとってくる
        //Playerタグが付いたオブジェクトを探す
        GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player");
        if(PlayerObj != null )
        {
            Player = PlayerObj.transform;
        }
        //回転を固定
        rbody.freezeRotation = true;
    }
    private void Update()
    {
        //地上判定
        bool onGround = Physics2D.CircleCast(transform.position,//発射位置
        0.2f,            //円の半径
        Vector2.down,    //発射方向
        0.0f,            //発射距離
        GroundLayer);    //検出するレイヤー

        if (onGround)
        {
            Jump();
        }

    }

  void Jump()
    {
        Vector2 jumpPw = new Vector2(0, Enemy_jump);//ジャンプさせりベクトルを作る
        rbody.AddForce(jumpPw, ForceMode2D.Force);

    }

    void FixedUpdate()
    {
       // if (Player != null)
         //   return;

        //プレイヤーのほうに向かす
        Vector2 direction=new Vector2(Player.position.x-transform.position.x,0).normalized;
        

        //その方向に常に移動
        rbody.linearVelocity =new Vector2( direction.x * Enemy_speed,0);

        //向きを反転
        if (Player.position.x>transform.position.x)
        {
            transform.localScale=new Vector2(-4,4);
        }
        else
        {
            transform.localScale=new Vector2(4,4);
        }
        

       
        
    }


}
