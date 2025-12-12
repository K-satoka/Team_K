using UnityEngine;
using UnityEngine.Audio;

public class icicle : MonoBehaviour
{
    public int ice_damage;

    Animator anim;
    private Rigidbody2D rb;
    public float landing_speed;
    public float lifetime = 15f;

    //se
    public AudioSource audioSouece;
    public AudioClip IceBreakSE;

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
            if (audioSouece != null && IceBreakSE != null)
                audioSouece.PlayOneShot(IceBreakSE);
            anim.Play("break");
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            if (audioSouece != null && IceBreakSE != null)
                audioSouece.PlayOneShot(IceBreakSE);

            anim.Play("break");
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag =="Enemy")
        {
            if (audioSouece != null && IceBreakSE != null)
                audioSouece.PlayOneShot(IceBreakSE);

            anim.Play("break");
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag=="BossATK")
        {
            if (audioSouece != null && IceBreakSE != null)
                audioSouece.PlayOneShot(IceBreakSE);

            anim.Play("break");
            Destroy(gameObject);
        }

    }

    
}
