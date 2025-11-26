using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//ENEMY

[RequireComponent (typeof (Rigidbody2D))]
public class boss : MonoBehaviour
{
    Rigidbody2D rbody;
    public float Enemy_speed = 2f;     // スピード
    public float Enemy_jump = 6f;      // ジャンプ力
    public float jumpTriggerDistance = 5f; // 検知距離
    public float GravityScale = 3.0f;  // 重力倍率
    public LayerMask GroundLayer;      // 地面レイヤー
 
    private Transform Player;
    private bool onGround = false;

    //SE
    public AudioSource audioSource;
    public AudioClip SlimejumpSE;

    private bool wasOnGround = false;//前フレームの設置状態
    void Start()
    {
        // Rigidbody2Dをとってくる
        rbody = GetComponent<Rigidbody2D>();//Rigidbody2Dからとってくる
        //Playerタグが付いたオブジェクトを探す
        GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player");
        if(PlayerObj != null )
        {
            Player = PlayerObj.transform;
        }
        //回転を固定
        rbody.freezeRotation = true;
        if (onGround = false)
        {
            rbody.gravityScale *= GravityScale;
        }
    }
    void Update()
    {
        if (Player == null) return;
        //float distanceToPlayer = Vector2.Distance(transform.position, Player.position);


        if (onGround)
        {
            //Debug.Log("ddddddddddddddddddddddd");
            Jump();
        }

    }

  void Jump()
    {
        

        rbody.AddForce(Vector2.up * Enemy_jump, ForceMode2D.Impulse);
        onGround = false;
      
    }

    void FixedUpdate()
    {
       
        Vector2 origin = (Vector2)transform.position + Vector2.down * 0.5f; // 足元あたり
        float radius = 0.3f;  // 半径
        float distance = 0.1f; // 判定距離

        onGround = Physics2D.CircleCast(
            origin,
            radius,
            Vector2.down,
            distance,
            GroundLayer
        );
        //プレイヤーのほうに向かす
        Vector2 direction=new Vector2(Player.position.x-transform.position.x,0).normalized;
        

         //その方向に常に移動
        rbody.linearVelocity = new Vector2(direction.x * Enemy_speed, rbody.linearVelocity.y);

        //向きを反転
        if (Player.position.x>transform.position.x)
        {
            transform.localScale=new Vector2(-4,4);
        }
        else
        {
            transform.localScale=new Vector2(4,4);
        }
        if (!wasOnGround && onGround && SlimejumpSE != null&&audioSource!=null)
        {
            audioSource.PlayOneShot(SlimejumpSE);
        }
        wasOnGround = onGround;
    }
   


}
