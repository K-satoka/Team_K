using UnityEngine;

public class thunder : MonoBehaviour
{
    public GameObject thunderlanding_Prefab;
    public Transform thunderlanding_Point;

    private Rigidbody2D rb;
    public float landing_speed;
    public float lifetime = 15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag=="Ground")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            ThunderlandingShot();

        }

    }

    void ThunderlandingShot()
    {
        Debug.Log("‚ ‚ ‚ ‚ ‚ ‚ ‚ ‚ ‚ ‚ ‚ ");
        Instantiate(thunderlanding_Prefab, thunderlanding_Point.position, thunderlanding_Point.rotation);
    }
}
