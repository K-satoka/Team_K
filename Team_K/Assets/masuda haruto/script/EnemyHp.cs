using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHp : MonoBehaviour
{
    public int Enemy_MAX_Hp = 10;//最大hp
    public int Enemy_Current_Hp;//現在のhp

    public int damageOnContact = 10;

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
        Debug.Log("敵が" + damage +"のダメージを受けた。残り:" +  Enemy_Current_Hp+ "/" + Enemy_MAX_Hp);
        if (hpSlider != null)
        {
            hpSlider.value = Enemy_Current_Hp;//HPバー更新
        }
        //SE
        if (audioSource != null && EnemydamageSE != null)
            audioSource.PlayOneShot(EnemydamageSE);
        audioSource.PlayOneShot(Enemydamage2SE);

        if ( Enemy_Current_Hp <= 0)
        {
            Die();
        }
    }

void Die()
    {
        Debug.Log("死んだぜ!");
        Debug.Log("保存 LastClearedStage = " + currentStageNumber);
        //次のステージの解放処理

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //現在のステージが最新だったら次を解放
       
        //動きを止めるテスト
        Rigidbody2D rb=GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }


        //---------------------

        Collider2D[]colliders = GetComponentsInChildren<Collider2D>();
        foreach (var col  in colliders)
        {
            col.enabled=(false);
        }

        PlayerPrefs.SetInt("LastClearedStage", currentStageNumber);
        
        PlayerPrefs.SetInt("CurrentStage", currentStageNumber);
        PlayerPrefs.Save();
        
        int stageUnlock = PlayerPrefs.GetInt("StageUnlock", 1);
        if (currentStageNumber==stageUnlock)
        {
            

            PlayerPrefs.SetInt("StageUnlock", stageUnlock + 1);
          
            PlayerPrefs.Save();
            Debug.Log("次に進めるぜ、相棒");
        }
        //SE再生

        if (audioSource != null && EnemyDieSE != null)
            audioSource.PlayOneShot(EnemyDieSE);

        Destroy(gameObject,2.0f);//ゲームオブジェクトを削除

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHp enemy = other.GetComponent<EnemyHp>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);

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
    //HP割合
    public float HPrate()
    {
        return (float)Enemy_Current_Hp / Enemy_MAX_Hp;
    }
}
