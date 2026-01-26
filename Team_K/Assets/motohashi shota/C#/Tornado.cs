using UnityEngine;
using UnityEngine.UIElements;

public class Tornado : MonoBehaviour
{
    public Transform player;

    [Header("追尾")]
    public float followDuration = 0.5f;
    public float followSpeed = 4f;

    [Header("ダメージ")]
    public int tormabo_damage = 10;

    private bool canFollow = false;
    private float timer;
    private bool isEnding = false;
    private Animator animator;
    private Collider2D col;

    public void Init(Transform target)
    {
        player = target;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isEnding) return;

        timer += Time.deltaTime;

        if (canFollow && timer <= followDuration && player != null)
        {
            
            //Vector2 pos = transform.position;
            //pos.x = Mathf.Lerp(pos.x, player.position.x, Time.deltaTime * followSpeed);
            //transform.position = pos;
        }
        else if (timer > followDuration)
        {
            StartEnd();
        }
    }

    void StartEnd()
    {
        if (isEnding) return;
        isEnding = true;

        // 当たり判定を止める
        if (col != null)
            col.enabled = false;

        // 消滅アニメ再生
        if (animator != null)
            animator.SetTrigger("End");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isEnding) return;

        if (col.CompareTag("Player"))
        {
            //Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            //if (rb != null)
            //{
            //    rb.velocity = Vector2.zero;
            //    rb.AddForce(new Vector2(-1f, 1f) * 500f);
            //}

            StartEnd();
        }
    }

    public void OnEndAnimationFinish()
    {
        Destroy(gameObject);
    }

    public void OnSpawnAnimationEnd()
    {
        canFollow = true;
    }
}
