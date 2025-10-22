using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public float CollisionCooldown = 0.0f;
    public int Player_MAX_Hp=100;
    public int Player_Current_Hp;


    public string gameOverSceneName = "GameOver";//ゲームオーバーシーン

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Player_Current_Hp-=1;
            Debug.Log(Player_Current_Hp);
        }
        if(Player_Current_Hp==0)
        {
            Destroy(gameObject);
            death();
        }
       
    }
    void death()
    {
        // シーンを切り替える
        SceneManager.LoadScene("GameOver");
    }

}
