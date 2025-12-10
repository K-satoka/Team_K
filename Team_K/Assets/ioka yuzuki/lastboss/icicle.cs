using UnityEngine;

public class icicle : MonoBehaviour
{
    public int ice_damage;

    Animator anim;
    private Rigidbody2D rb;
    public float landing_speed;
    public float lifetime = 15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.Play("break");
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            anim.Play("break");
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag =="Enemy")
        {
            anim.Play("break");
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag=="BossATK")
        {
            anim.Play("break");
            Destroy(gameObject);
        }

    }

    
}
