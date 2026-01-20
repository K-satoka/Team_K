using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class firebullet : MonoBehaviour
{
    private Rigidbody2D rb;

    public int fire_damage=1;//青い炎のダメージ
    public float speed = 50.0f;
    public float lifetime = 15f;//炎の強制消滅

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
            reverse();
        }

            Destroy(gameObject, lifetime);//時間経過で玉が消える
    }

    void reverse()//反転
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1; // x を反転
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("aguhguiraguai");
            Destroy(gameObject);//あたったらたまがきえる
        }        
        else if(collision.gameObject.tag =="Wall")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag =="Enemy")
        {
            Destroy(gameObject);
        }
    }
}