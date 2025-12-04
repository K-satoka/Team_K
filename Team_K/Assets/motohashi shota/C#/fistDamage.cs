using UnityEngine;

public class fistDamage : MonoBehaviour
{
    public int damage2 = 10; // ダメージ量
    public Animator animator;
    
    //追加
    private Collider2D col;

    private void Start()
    {
        animator = GetComponent<Animator>(); 
        col=GetComponent<Collider2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("当たったぁ！");

           // プレイヤーにHPスクリプトがあれば呼び出す
           //PlayerHealth hp = other.GetComponent<PlayerHealth>();
           // if (hp != null)
           // {
           //     hp.TakeDamage(damage);
           // }

           
            // こぶしを消す
            animator.SetTrigger("HIT");
            Destroy(gameObject,0.3f);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("地面に当たったぁ！");

            
            // プレイヤーにHPスクリプトがあれば呼び出す
            //PlayerHealth hp = other.GetComponent<PlayerHealth>();
            //if (hp != null)
            //{
            //    hp.TakeDamage(damage);
            //}

            // こぶしを消す
            animator.SetTrigger("HIT");
            Destroy(gameObject, 0.3f);
        }
    }
}
