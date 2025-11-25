using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class firebullet : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 50.0f;
    public float lifetime = 15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        if(transform.position.x>0)
        {
            rb.linearVelocity = -transform.right * speed;
        }
        else
        {
            rb.linearVelocity = transform.right * speed;
            //Vector2 scale = transform.localScale;
            //scale.x *= -1; // X ÇîΩì]
            //transform.localScale = scale;
        }

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