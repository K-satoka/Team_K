using UnityEngine;
using UnityEngine.Audio;

public class icicle : MonoBehaviour
{
    Animator anim;

    public int ice_damage;//Ç¬ÇÁÇÁÉ_ÉÅÅ[ÉW
    private Rigidbody2D rb;
    public float landing_speed;
    public float lifetime = 15f;

    //se----------------------------
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
            icebreak();
        }
        else if (collision.gameObject.tag == "Ground")
        {
            icebreak();
        }
        else if(collision.gameObject.tag =="Enemy")
        {
            icebreak();
        }
        else if(collision.gameObject.tag=="BossATK")
        {
            icebreak();
        }

    }
    void icebreak()
    {
        if (audioSouece != null && IceBreakSE != null)
            audioSouece.PlayOneShot(IceBreakSE);
        anim.Play("break");
        Destroy(gameObject, 0.1f);
    }

}
