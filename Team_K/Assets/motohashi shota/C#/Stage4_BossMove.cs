using UnityEngine;
using System.Collections;

public class Stage4_BossMove : MonoBehaviour
{
    [Header("ターゲット")]
    public Transform player;

    [Header("移動パラメータ")]
    public float moveSpeed = 3f;
    public float stopDistance = 2f;

    //Rigidbody、アニメーター、スプライトレンダラー用
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
    {
        //アニメーター、RiGidBody取得
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        //プレイヤーがないと値を返すねー
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < stopDistance)
        {
            //近づきすぎたら止まるねー
            rb.velocity = new Vector2(0,rb.velocity.y);
            anim.SetBool("isMoving", false);
        }
        else
        {
            // プレイヤーを追いかける（横移動のみ）
            float dirX = player.position.x - transform.position.x; // 左右の差を求める
            if (dirX != 0)
            {
                sr.flipX = dirX > 0;//プレイヤーの方に行く
            }

            dirX = Mathf.Sign(dirX); // 左右の向きだけ -1 or 1 にする(matif.signで正か０なら１を、負なら-1を返す)

            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            anim.SetBool("isMoving", true);
        }
    }
}
