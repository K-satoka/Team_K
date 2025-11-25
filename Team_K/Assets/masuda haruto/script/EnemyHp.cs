using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHp : MonoBehaviour
{
    public int Enemy_MAX_Hp = 10;//最大hp
    public int Enemy_Current_Hp;//現在のhp

    public int damageOnContact = 10;

    public Slider hpSlider;
   

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
        if (Enemy_Current_Hp < 0) Enemy_Current_Hp = 0;//HPがマイナスにならないように
        Debug.Log("敵が" + damage +"のダメージを受けた。残り:" +  Enemy_Current_Hp+ "/" + Enemy_MAX_Hp);
        if (hpSlider != null)
        {
            hpSlider.value = Enemy_Current_Hp;//HPバー更新
        }
        if ( Enemy_Current_Hp <= 0)
        {
            Die();
        }
    }

void Die()
    {
        Debug.Log("死んだぜ!");

        //次のステージの解放処理
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //現在のステージが最新だったら次を解放
        int stageUnlock = PlayerPrefs.GetInt("StageUnlock", 1);
        if (currentSceneIndex>=stageUnlock)
        {
            PlayerPrefs.SetInt("StageUnlock", stageUnlock + 1);
            PlayerPrefs.Save();
            Debug.Log("次に進めるぜ、相棒");
        }

        
            Destroy(gameObject);//ゲームオブジェクトを削除
       
        //すてせれに戻る
        FadeManager.Instance.LoadScene("Reward",1.0f);
    
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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("aaaaa");

    //        TakeDamage(damageOnContact);
    //    }
    //}
}
