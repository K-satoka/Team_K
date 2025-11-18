using Unity.VisualScripting;
using UnityEngine;

public class firebullet : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 50.0f;
    public float lifetime = 30f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = -transform.right * speed;
        Destroy(gameObject, lifetime);//éûä‘åoâﬂÇ≈ã Ç™è¡Ç¶ÇÈ
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ç†Ç†Ç†Ç†Ç†");
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);//Ç†ÇΩÇ¡ÇΩÇÁÇΩÇ‹Ç™Ç´Ç¶ÇÈ
        }
    }
}