using UnityEngine;

public class maou : MonoBehaviour
{
    Rigidbody2D rb;
    //fire-------------------------------
    public GameObject fire_bulletPrefab;
    public GameObject thunder_bulletPrefab;
    public GameObject icicle_bulletPrefab;
    public Transform firePoint;
    public Transform thunderPoint;
    public Transform iciclePoint;
    public float shootInterval = 2f;
    //------------------------------------
    //telep
    public float detecDistance = 5f;//反応する距離
    public float detecTime = 2f;    //↑にいたら反応するまでの時間
    private int firecount;
    Animator anim;

    private float timer;
    private float attacktimer;
    private float timeout=0.2f;
    public Transform Player;

    public AudioSource audioSource;
    public AudioClip MAouAttackSE;
    public AudioClip TereportSE;

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
        //------------------------------------------

        
        if (distance<=detecDistance)
        {
           
            //timestart
            timer += Time.deltaTime;
            if(timer > detecTime)
            {
                if (audioSource != null && TereportSE != null)
                    audioSource.PlayOneShot(TereportSE);
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
                float rand=Random.Range(1,3);
                if (rand ==1)
                {
                    anim.Play("maoufireshot");
                    FireShoot();
                }
                else
                {
                    icicleShot();
                }
                    

                


                timer = 0f;
            }
        }
        //fireshot
       
        
           
        
    }

    void FireShoot()
    {
            Instantiate(fire_bulletPrefab, firePoint.position, firePoint.rotation);
            
        if (audioSource != null && MAouAttackSE != null)
            audioSource.PlayOneShot(MAouAttackSE);

    }

    //void ThunderShot()
    //{
    //    Instantiate(thunder_bulletPrefab, thunderPoint.position, thunderPoint.rotation);
    //}
    void icicleShot()
    {
        Instantiate(icicle_bulletPrefab,iciclePoint.position, iciclePoint.rotation);
    }

    void teleport()
    {
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