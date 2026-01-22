using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

[RequireComponent (typeof (Rigidbody2D))]
public class boss : MonoBehaviour
{
    EnemyHp hp;
    Rigidbody2D rb;
    public float Enemy_speed = 2f;     // スピード
    public float Enemy_jump = 6f;      // ジャンプ力
    public float jumpTriggerDistance = 5f; // 検知距離
    public float GravityScale = 3.0f;  // 重力倍率
    public LayerMask GroundLayer;      // 地面レイヤー

    public float detecDistance = 5f;//反応する距離
    private Transform Player;//プレイヤーの位置確認
    private bool onGround = false;
    Animator anim;
    //SE------------------------------------------------
    public AudioSource audioSource;
    public AudioClip SlimejumpSE;

    public int damage=10;  //攻撃力

    private bool wasOnGround = false;//前フレームの設置状態
    void Start()
    {
        hp = GetComponent<EnemyHp>();
        anim =GetComponent<Animator>();
        // Rigidbody2Dをとってくる
        rb = GetComponent<Rigidbody2D>();//Rigidbody2Dからとってくる
        //Playerタグが付いたオブジェクトを探す
        GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player");
        if(PlayerObj != null )
        {
            Player = PlayerObj.transform;
        }
        //回転を固定
        rb.freezeRotation = true;
        if (onGround = false)
        {
            rb.gravityScale *= GravityScale;
        }
    }
    void Update()
    {
        // プレイヤーまでの距離を計算
        float Playerdistance = Vector2.Distance(transform.position, Player.position);
        //------------------------------------------
        if (Player == null) return;

        float hprate = hp.HPrate();//HP割合
        if (hprate>0.7f)
        {
            pattern1();
        }
        else if(hprate>0.3f)
        {
            pattern2();
        }
        else
        {
            pattern3();
        }
    }

    void pattern1()
    {
        float Playerdistance = Vector2.Distance(transform.position, Player.position);
        if (onGround)
        {
            if (Playerdistance <= detecDistance)
            {
                Jump();
            }
            else if (Playerdistance > detecDistance)
            {
                anim.Play("slime_waiting");
            }
        }
    }
    void pattern2()
    {
        float Playerdistance = Vector2.Distance(transform.position, Player.position);
        if (onGround)
        {
            if (Playerdistance <= detecDistance)
            {

                Jump2();
            }
            else if (Playerdistance > detecDistance)
            {
                anim.Play("slime_waiting");
            }
        }
    }
    void pattern3()
    {
        float Playerdistance = Vector2.Distance(transform.position, Player.position);
        if (onGround)
        {
            if (Playerdistance <= detecDistance)
            {

                Jump3();
            }
            else if (Playerdistance > detecDistance)
            {
                anim.Play("slime_waiting");
            }
        }
    }
    void Jump()
    {
        rb.AddForce(Vector2.up * Enemy_jump, ForceMode2D.Impulse);
        onGround = false;
    }
    void Jump2()
    {
        rb.AddForce(Vector2.up * Enemy_jump*1.5f, ForceMode2D.Impulse);
        onGround = false;
    }

    void Jump3()
    {
        rb.AddForce(Vector2.up * Enemy_jump*2f, ForceMode2D.Impulse);
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

       if(onGround)
        {
            //その方向に常に移動
            rb.linearVelocity = new Vector2(direction.x * Enemy_speed, rb.linearVelocity.y);
        }
       
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
