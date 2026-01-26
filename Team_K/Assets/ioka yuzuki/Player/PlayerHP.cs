using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHP : MonoBehaviour
{
    public float CollisionCooldown = 0.0f;
    public int Player_MAX_Hp=100;
    public int Player_Current_Hp;
    public float knock_back=10f;

    public static string previousSceneName;

    public float invincibleTime = 1.0f; // 無敵時間
    private bool isInvincible = false;

    Rigidbody2D rb;

    public Slider PlayerhpSlider;
    //ゲームオーバーシーン--------------------
    public string gameOverSceneName = "GameOver";

    //SE--------------------------------------
    public AudioSource audioSource;
    public AudioClip PlayerDamageSE;
    public AudioClip PlayerDieSE;


    private bool isDead = false;

    void Start()
    {
        Player_Current_Hp +=PlayerData.Instance.maxHP_Up;

        if (PlayerhpSlider != null)
        {
            PlayerhpSlider.maxValue = Player_Current_Hp;
            PlayerhpSlider.value = Player_Current_Hp;
        }
        
        rb = GetComponent<Rigidbody2D>();

        Debug.Log($"�J�[�h�ő��������vHP: {PlayerData.Instance.maxHP_Up}");
        Debug.Log($"���݂̍ő�HP: {Player_Current_Hp}");
    }


    void Update()
    {
        if (PlayerhpSlider != null)
        {
            PlayerhpSlider.value = Player_Current_Hp;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isInvincible) return;

        boss bossScript = collision.gameObject.GetComponent<boss>();
        fistDamage fistDamageScript = collision.gameObject.GetComponent<fistDamage>();
        S3Atk s3AtkScript=collision.gameObject.GetComponent<S3Atk>();
        SnowAttack snowAttackScript=collision.gameObject.GetComponent<SnowAttack>();
        firebullet firebulletScript=collision.gameObject.GetComponent<firebullet>();
        icicle icicleScript=collision.gameObject.GetComponent<icicle>();
       
        //stage1ボスダメージ
        if (bossScript != null)
        {
            int dmg = bossScript.damage;         
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Player_Current_Hp -= dmg;
                GetComponent<DamageFlash>().Flash();
                StartCoroutine(InvincibleCoroutine());
            }
        }
        
        //stage2ボス攻撃ダメージ
        if (fistDamageScript != null)
        {
            int dmg2 = fistDamageScript.damage2;
            if (collision.gameObject.CompareTag("BossATK"))
            {
                Player_Current_Hp -= dmg2;
                GetComponent<DamageFlash>().Flash();
                StartCoroutine(InvincibleCoroutine());
            }
        }
        //ステージ3ダメージ
        if (s3AtkScript != null)
        {
            int st3_dmg = s3AtkScript.st3_damage;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Player_Current_Hp -= st3_dmg;
                GetComponent<DamageFlash>().Flash();
                StartCoroutine(InvincibleCoroutine());
            }
        }
        //st4
        if (snowAttackScript != null)
        {
            int st4_dmg = snowAttackScript.s4_damage;
            if (collision.gameObject.CompareTag("BossATK"))
            {
                Player_Current_Hp -= st4_dmg;
                GetComponent<DamageFlash>().Flash();
                StartCoroutine(InvincibleCoroutine());
            }
        }
        //st5fire
        if (firebulletScript != null)
        {
            int st5_dmg = firebulletScript.fire_damage;
            if (collision.gameObject.CompareTag("BossATK"))
            {
                Player_Current_Hp -= st5_dmg;
                GetComponent<DamageFlash>().Flash();
                StartCoroutine(InvincibleCoroutine());
            }
        }
        //st5ice
        if (icicleScript != null)
        {
            int st5_icedmg = icicleScript.ice_damage;
            if (collision.gameObject.CompareTag("BossATK"))
            {
                Player_Current_Hp -= st5_icedmg;
                GetComponent<DamageFlash>().Flash();
                StartCoroutine(InvincibleCoroutine());
            }
        }
        //SE----------------------------------------------------------------------------------------
        if (collision.gameObject.CompareTag("Enemy")||collision.gameObject.CompareTag("BossATK"))
        {
            if (audioSource != null && PlayerDamageSE != null)
                audioSource.PlayOneShot(PlayerDamageSE);
            StartCoroutine(InvincibleCoroutine());
            Debug.Log(Player_Current_Hp);
        }
        //死亡------------------------------------------
        if (Player_Current_Hp <= 0 && !isDead)
        {
            isDead = true;
            Destroy(gameObject, 1.5f);
            death();
        }
    }
    void death()
    {
        if (audioSource != null && PlayerDieSE != null)
            audioSource.PlayOneShot(PlayerDieSE);

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0f;
            rb.simulated = false;
        }

        EnemyHp[] enemies = FindObjectsOfType<EnemyHp>();
        foreach (var enemy in enemies)
        {
            if(enemy!=null)
            enemy.StopMoment();
        }

        FadeManager.Instance.LoadScene("GameOver", 1.0f);
    }

    IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }


}
