
using Unity.VisualScripting;

using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHP : MonoBehaviour
{
    public float CollisionCooldown = 0.0f;
    public int Player_MAX_Hp=100;
    public int Player_Current_Hp;
    public float knock_back=10f;

    public static string previousSceneName;//�O�̃V�[������ۑ�

    Rigidbody2D rbody;


    public Slider PlayerhpSlider;

    public string gameOverSceneName = "GameOver";//�Q�[���I�[�o�[�V�[��

    //SE
    public AudioSource audioSource;
    public AudioClip PlayerDamageSE;
    public AudioClip PlayerDieSE;

    //���S���d�h�~
    private bool isDead = false;

    void Start()
    {
        Player_Current_Hp =Player_MAX_Hp + PlayerData.Instance.maxHP_Up;

        // �X���C�_�[������
        if (PlayerhpSlider != null)
        {
            PlayerhpSlider.maxValue = Player_Current_Hp;
            PlayerhpSlider.value = Player_Current_Hp;
        }

     rbody = GetComponent<Rigidbody2D>();



        // �������̃��O
        Debug.Log($"�J�[�h�ő��������vHP: {PlayerData.Instance.maxHP_Up}");
        Debug.Log($"���݂̍ő�HP: {Player_Current_Hp}");
    }

    // Update is called once per frame
    void Update()
    {
        // �X���C�_�[����Ɍ���HP�ɍ��킹��
        if (PlayerhpSlider != null)
        {
            PlayerhpSlider.value = Player_Current_Hp;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        boss bossScript = collision.gameObject.GetComponent<boss>();
        fistDamage fistDamageScript = collision.gameObject.GetComponent<fistDamage>();
        S3Atk s3AtkScript=collision.gameObject.GetComponent<S3Atk>();
        //stage1ボスダメージ
        if (bossScript != null)
        {
            int dmg = bossScript.damage;         
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Player_Current_Hp -= dmg;
            }
        }
        //stage2ボス攻撃ダメージ
        if (fistDamageScript != null)
        {
            int dmg2 = fistDamageScript.damage2;
            if (collision.gameObject.CompareTag("BossATK"))
            {
                Player_Current_Hp -= dmg2;
            }
        }
        //ステージ3ダメージ
        if (s3AtkScript != null)
        {
            int st3_dmg = s3AtkScript.st3_damage;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Player_Current_Hp -= st3_dmg;
            }
        }
        //SE----------------------------------------------------------------------------------------
        if (collision.gameObject.CompareTag("Enemy")||collision.gameObject.CompareTag("BossATK"))
        {
            if (audioSource != null && PlayerDamageSE != null)
                audioSource.PlayOneShot(PlayerDamageSE);

            Debug.Log(Player_Current_Hp);
        }
        //------------------------------------------------
        //死亡
        if (Player_Current_Hp <= 0 && !isDead)
        {
            isDead = true;
            Destroy(gameObject, 1.5f);
            death();
        }
        //------------------------------------------
        //ボス接触時ダメージ_____EnemyTag変更予定
        /*
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Player_Current_Hp -= 1;
            if (audioSource != null && PlayerDamageSE != null)
                audioSource.PlayOneShot(PlayerDamageSE);

            Debug.Log(Player_Current_Hp);
        }
        if (Player_Current_Hp <= 0 && !isDead)
        {
            isDead = true;
            Destroy(gameObject, 1.5f);
            death();
        }
        */
    }

    //�U�����󂯂��Ƃ��ɌĂ�


    //public void TakeDamage(int damage)
    //{
    //    Player_Current_Hp -= damage;
    //    if (Player_Current_Hp < 0) Player_Current_Hp = 0;//HP���}�C�i�X�ɂȂ�Ȃ��悤��

    //    if (Player_Slider != null)
    //    {
    //        Player_Slider.value = Player_Current_Hp;//HP�o�[�X�V
    //    }

    //}
    //private void Awake()
    //{
    //    //���[�񂫂肩�����ɏ����Ȃ��悤��
    //    DontDestroyOnLoad(this.gameObject);
    //}

    void death()
    {
       
        if (audioSource != null && PlayerDieSE != null)
            audioSource.PlayOneShot(PlayerDieSE);

        if (rbody != null)
        {
            rbody.linearVelocity = Vector2.zero;
            rbody.angularVelocity = 0f;
            rbody.gravityScale = 0f;
            rbody.simulated = false;
        }

        // �G�̓������~�߂�
        EnemyHp[] enemies = FindObjectsOfType<EnemyHp>();
        foreach (var enemy in enemies)
        {
            if(enemy!=null)
            enemy.StopMoment();
        }

        ////���̃V�[����ۑ�
        //previousSceneName=SceneManager.GetActiveScene().name;

        // �V�[����؂�ւ���
        FadeManager.Instance.LoadScene("GameOver", 1.0f);
    }

    /*public void IncreaseMaxHP(int aumount)
    {
        Player_MAX_Hp += aumount;
        Player_Current_Hp += aumount;
        Debug.Log("�ő�HP��" + aumount + "�������您��B���̍ő�HP��" + Player_MAX_Hp);
    }
    */
}
