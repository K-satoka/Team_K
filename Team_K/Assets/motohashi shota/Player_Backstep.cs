using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Backstep : MonoBehaviour
{
    [Header("バックステップ設定")]
    public float stepSpeed = 10f;            // バックステップ速度
    public float stepDuration = 0.15f;       // バックステップ時間
    public float stepCooldown = 1f;          // クールタイム

    private bool isStepping = false;         // ステップ中かどうか
    private bool isCooldown = false;         // クールタイム中かどうか
    private Vector2 stepDirection;           // ステップ方向

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // LeftShiftキーで回避
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame && !isStepping && !isCooldown)
        {
            StartBackstep();
        }
    }

    //==============================
    // バックステップ開始
    //==============================
    void StartBackstep()
    {
        isStepping = true;
        isCooldown = true;

        // プレイヤーの向いている方向の反対へ移動
        float facing = Mathf.Sign(transform.localScale.x);
        stepDirection = new Vector2(facing, 0f);

        // ★ 押した瞬間に速度を与えて動かす
        rb.velocity = stepDirection * stepSpeed;

        // ★ アニメーション開始（Trigger方式）
        animator?.SetTrigger("Backstep");

        // 無敵レイヤーに変更
        gameObject.layer = LayerMask.NameToLayer("PlayerInvincible");

        // クールタイム解除予約
        CancelInvoke("ResetCooldown");
        Invoke("ResetCooldown", stepCooldown);

        // ★ バックステップ終了予約（アニメ時間と合わせる）
        Invoke("EndBackstep", stepDuration);
    }

    //==============================
    // バックステップ終了
    //==============================
    void EndBackstep()
    {
        isStepping = false;

        // 移動を止める
        rb.velocity = Vector2.zero;

        // 無敵解除
        gameObject.layer = LayerMask.NameToLayer("");

        // デバッグ確認用
        // Debug.Log("バックステップ終了");
    }

    //==============================
    // クールタイム解除
    //==============================
    void ResetCooldown()
    {
        isCooldown = false;
    }
}
