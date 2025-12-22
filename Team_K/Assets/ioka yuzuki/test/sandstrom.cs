using UnityEngine;




public class sandstrom : MonoBehaviour
{
    public float sp = 1;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity=new Vector2(sp*-1,0);

    }
}
