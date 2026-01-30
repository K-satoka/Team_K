using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

//チュートリアル用のサンドバッグ

public class tutorial : MonoBehaviour
{
    public int Enemy_MAX_Hp = 10;//最大hp
    public int Enemy_Current_Hp;//現在のhp

    public int damageOnContact = 10;

    public float invincibleTime = 0.2f; // 無敵時間
    private bool isInvincible = false;

    public Slider hpSlider;

    public int currentStageNumber = 1;

    public AudioSource audioSource;
    public AudioClip EnemydamageSE;
    public AudioClip Enemydamage2SE;
    public AudioClip EnemyDieSE;

    public int XP = 1;
    void Start()
    {
        Enemy_Current_Hp = Enemy_MAX_Hp;   //初期値を最大値に設定

        if (hpSlider != null)
        {
            hpSlider.maxValue = Enemy_MAX_Hp;//スライダーの最大値
            hpSlider.value = Enemy_Current_Hp;//初期値
        }

    }

    public void TakeDamage(int damage)
    {
        Enemy_Current_Hp -= damage;

        GetComponent<DamageFlash>().Flash();

        if (Enemy_Current_Hp < 0)
            Enemy_Current_Hp = 0;//HPがマイナスにならないように
        if (hpSlider != null)
        {
            hpSlider.value = Enemy_Current_Hp;//HPバー更新
        }
        //SE
        if (audioSource != null && EnemydamageSE != null)
            audioSource.PlayOneShot(EnemydamageSE);
        audioSource.PlayOneShot(Enemydamage2SE);

        if (Enemy_Current_Hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        //---------------------

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (var col in colliders)
        {
            col.enabled = (false);
        }

        PlayerPrefs.SetInt("LastClearedStage", currentStageNumber);

        PlayerPrefs.SetInt("CurrentStage", currentStageNumber);
        PlayerPrefs.Save();

        int stageUnlock = PlayerPrefs.GetInt("StageUnlock", 1);
        if (currentStageNumber == stageUnlock)
        {


            PlayerPrefs.SetInt("StageUnlock", stageUnlock + 1);

            PlayerPrefs.Save();
            Debug.Log("次に進めるぜ、相棒");
        }
        //SE再生

        if (audioSource != null && EnemyDieSE != null)
            audioSource.PlayOneShot(EnemyDieSE);

        

        if (currentStageNumber == 5)
        {
            FadeManager.Instance.LoadScene("Ending", 1.0f);
        }
        else
        {
            // 通常は Reward へ
            FadeManager.Instance.LoadScene("Reward", 1.0f);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isInvincible) return;

        if (other.CompareTag("Enemy"))
        {
            EnemyHp enemy = other.GetComponent<EnemyHp>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);
                StartCoroutine(InvincibleCoroutine());
            }
        }
    }
    //テスト敵のプレーや死亡の時の停止支持
    public void StopMoment()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            try
            {
                rb.linearVelocity = Vector2.zero;
                rb.gravityScale = 0f;
                rb.angularVelocity = 0f;
                rb.simulated = false;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("StopMomentで例外発生: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("StopMoment: Rigidbody2Dが見つかりません");
        }

        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.enabled = false; // アニメも止める
        }


    }
    IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }
}
