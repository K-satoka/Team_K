using Unity.VisualScripting;
using UnityEngine;

public class maou : MonoBehaviour
{
    Rigidbody2D rb;
    //fire-------------------------------
    EnemyHp hp;
    public GameObject fire_bulletPrefab;
    public GameObject thunder_bulletPrefab;
    public GameObject icicle_bulletPrefab;
    public GameObject black_firePrefab;
    public Transform firePoint;
    public Transform thunderPoint;
    public Transform iciclePoint;
    public Transform bulletPoint1;
    public Transform bulletPoint2;
    public Transform bulletPoint3;
    public Transform bulletPoint4;
    public Transform bulletPoint5;
    public Transform bulletPoint6;
    public Transform bulletPoint7;
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
    public AudioClip IceSE;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = GetComponent<EnemyHp>();
        anim =GetComponent<Animator>();
        
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
        //HP�̊���������Ă���
        float hprate = hp.HPrate();

        if (hprate >= 0.7f)
        {
            pattern1();
        }
        else if(hprate>=0.5f&&hprate<=0.6f)
        {
            pattern2();
        }
        else if(hprate >= 0.3f)
        {
            pattern3();
        }
        else
        {
         pattern4();   
        }

    }
    void pattern1()
    {
        float distance = Vector2.Distance(transform.position, Player.position);
        if (distance <= detecDistance)
        {

            //timestart
            timer += Time.deltaTime;
            if (timer > detecTime)
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
                float rand = Random.Range(1, 3);
                if (rand == 1)
                {
                    anim.Play("maoufireshot");
                    FireShoot();
                }
                else if(rand == 2)
                {
                    anim.Play("maoufireshot");
                    icicleShot();
                }
                else
                {
                    anim.Play("maoufireshot");
                    Black_fire();
                }
                    timer = 0f;
            }
        }
    }

    void pattern2()
    {
        timer += Time.deltaTime;
        if (timer >= shootInterval)
        {
            float rand = Random.Range(1, 3);
            if (rand == 1)
            {
                anim.Play("maoufireshot");
                FireShoot();
            }
            else
            {
                anim.Play("maoufireshot");
                Black_fire();
            }
            timer = 0f;
        }
    }
    void pattern3()
    {
        float distance = Vector2.Distance(transform.position, Player.position);
        if (distance <= detecDistance)
        {

            //timestart
            timer += Time.deltaTime;
            if (timer > detecTime)
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
                float rand = Random.Range(1, 9);
                if (rand <=3)
                {
                    anim.Play("maoufireshot");
                    FireShoot();
                }
                else if (rand <=6)
                {
                    anim.Play("maoufireshot");
                    icicleShot();
                }
                else
                {
                    anim.Play("maoufireshot");
                    Black_fire();
                }
                timer = 0f;
            }
        }
    }

    void pattern4()
    {
        float distance = Vector2.Distance(transform.position, Player.position);
        if (distance <= detecDistance)
        {

            //timestart
            timer += Time.deltaTime;
            if (timer > detecTime)
            {

                teleport();
                anim.Play("maouteleport");
                timer = 0f;
            }
        }
        else if (distance >= detecDistance)
        {//プレイヤーが離れたらリセット
            timer += Time.deltaTime;
            if (timer >= shootInterval-0.2f)
            {
                float rand = Random.Range(1, 11);
                if (rand <= 3)
                {
                    anim.Play("maoufireshot");
                    FireShoot();
                }
                else if (rand <= 6)
                {
                    anim.Play("maoufireshot");
                    icicleShot();
                }
                else if(rand <= 9)
                {
                    anim.Play("maoufireshot");
                    Black_fire();
                }
                else 
                {
                    teleport();
                    anim.Play("maouteleport");
                }
                    timer = 0f;
            }
        }
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
        if (audioSource != null && IceSE != null)
            audioSource.PlayOneShot(IceSE);
        Instantiate(icicle_bulletPrefab,iciclePoint.position, iciclePoint.rotation);
    }

    void Black_fire()
    {
        Instantiate(black_firePrefab, bulletPoint1.position, bulletPoint1.rotation);
        Instantiate(black_firePrefab, bulletPoint2.position, bulletPoint2.rotation);
        Instantiate(black_firePrefab, bulletPoint3.position, bulletPoint3.rotation);
        Instantiate(black_firePrefab, bulletPoint4.position, bulletPoint4.rotation);
        Instantiate(black_firePrefab, bulletPoint5.position, bulletPoint5.rotation);
        Instantiate(black_firePrefab, bulletPoint6.position, bulletPoint6.rotation);
        Instantiate(black_firePrefab, bulletPoint7.position, bulletPoint7.rotation);
    }
    void teleport()
    {
        if (transform.position.x > 0)
        {
            if (audioSource != null && TereportSE != null)
                audioSource.PlayOneShot(TereportSE);
            transform.position = new Vector2(-84, -34);
        }
        else
        {
            if (audioSource != null && TereportSE != null)
                audioSource.PlayOneShot(TereportSE);
            transform.position = new Vector2(84, -34);
        }

    }
}