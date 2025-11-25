using UnityEngine;

public class maou : MonoBehaviour
{
    Rigidbody2D rb;
    //fire-------------------------------
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 2f;
    //------------------------------------
    //telep
    public float detecDistance = 5f;//反応する距離
    public float detecTime = 2f;    //↑にいたら反応するまでの時間

    Animator anim;

    private float timer;
    public Transform Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //常に中央へ------------------
        Vector3 center = Camera.main.ScreenToWorldPoint(
          new Vector3(Screen.width / 2, Screen.height / 2, 0)
      );

        transform.localScale = new Vector3(
            center.x > transform.position.x ? -4 : 4,
            4,
            1
        );
        //---------------------------------

        // プレイヤーまでの距離を計算
        float distance = Vector2.Distance(transform.position, Player.position);
        Debug.Log(distance);
        //------------------------------------------


        if (distance<=detecDistance)
        {
           
            //timestart
            timer += Time.deltaTime;
            if(timer > detecTime)
            {
                
                teleport();
                anim.Play("maouteleport");
                timer = 0f;
            }
        }
        else if (distance >= detecDistance)
        {//プレイヤーが離れたらリセット
            timer += Time.deltaTime;
            if (timer >= shootInterval)
            {
                Shoot();
                anim.Play("maoufire");
                timer = 0f;
            }
        }
        //fireshot
       
        
           
        
    }

    void Shoot()
    {
        //Debug.Log("sssssssssssssss");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);


    }


    void teleport()
    {
        Debug.Log("aaaaaaaaaaaaaa");
        if (transform.position.x > 0)
        {
            transform.position = new Vector2(-84, -34);
        }
        else
        {
            transform.position = new Vector2(84, -34);
        }

    }
}