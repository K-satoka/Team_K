using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class back_st3 : MonoBehaviour
{

    public float speed = 1f;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(speed * -1f, speed*-1f);
        if (transform.position.x < -160)
        {
            transform.position = new Vector2(160, transform.position.y * 1);
        }
    }
}

