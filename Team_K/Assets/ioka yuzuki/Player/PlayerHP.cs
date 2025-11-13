
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

    public static string previousSceneName;//前のシーン名を保存

    Rigidbody2D rbody;


    public Slider PlayerhpSlider;

    public string gameOverSceneName = "GameOver";//ゲームオーバーシーン

    void Start()
    {
        Player_Current_Hp = Player_MAX_Hp;

        // スライダー初期化
        if (PlayerhpSlider != null)
        {
            PlayerhpSlider.maxValue = Player_MAX_Hp;
            PlayerhpSlider.value = Player_Current_Hp;
        }

     rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // スライダーを常に現在HPに合わせる
        if (PlayerhpSlider != null)
        {
            PlayerhpSlider.value = Player_Current_Hp;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Player_Current_Hp -= 1;
            Debug.Log(Player_Current_Hp);
        }

        if (Player_Current_Hp == 0)
        {
            Destroy(gameObject);
            death();
        }

    }

    //攻撃を受けたときに呼ぶ


    //public void TakeDamage(int damage)
    //{
    //    Player_Current_Hp -= damage;
    //    if (Player_Current_Hp < 0) Player_Current_Hp = 0;//HPがマイナスにならないように

    //    if (Player_Slider != null)
    //    {
    //        Player_Slider.value = Player_Current_Hp;//HPバー更新
    //    }

    //}
    //private void Awake()
    //{
    //    //しーんきりかえ時に消えないように
    //    DontDestroyOnLoad(this.gameObject);
    //}

    void death()
    {
        ////今のシーンを保存
        //previousSceneName=SceneManager.GetActiveScene().name;
        
        // シーンを切り替える
        SceneManager.LoadScene("GameOver");
    }

    public void IncreaseMaxHP(int aumount)
    {
        Player_MAX_Hp = aumount;
        Player_Current_Hp += aumount;
        Debug.Log("最大HPが" + aumount + "増えたよおん。今の最大HPは" + Player_MAX_Hp);
    }

}
