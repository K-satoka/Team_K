using UnityEngine;

public class TestAttack : MonoBehaviour
{
    public float jumpForceY = 10f;
    public float jumpForceX = 5f;               // ここを修正
    public float stompRadius = 2f;
    public LayerMask playerLayer;               // 地面判定用レイヤー
    public Transform groundChack;               // 地面判定用の位置
    public float groundCheckRadius = 0.2f;
    public float jumpCooldown = 2f;             // ジャンプのクールダウン時間

    private Rigidbody2D rb;
    private Transform player;
    private bool isGrounded = true;
    private float nextJumpTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;  // プレイヤーの取得修正
    }

    void Update()
    {
        // 地面にいるかどうか判定
        Collider2D groundCollider = Physics2D.OverlapCircle(groundChack.position, groundCheckRadius, playerLayer);
        isGrounded = groundCollider != null;

        // 地面にいて、クールダウンが終わっていたらジャンプ
        if (isGrounded && Time.time > nextJumpTime)
        {
            JumpAtPlayer();
          
        }
    }

    void JumpAtPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 jumpVelocity = new Vector2(direction.x * jumpForceX, jumpForceY);
        rb.linearVelocity = jumpVelocity;    // 速度設定
    }
}
