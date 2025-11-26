using UnityEngine;

public class SnowAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int damage = 1; // ダメージ量

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("当たったぁ！");

            // プレイヤーにHPスクリプトがあれば呼び出す
            //PlayerHealth hp = other.GetComponent<PlayerHealth>();
            //if (hp != null)
            //{
            //    hp.TakeDamage(damage);
            //}

            // こぶしを消す
            Destroy(gameObject, 0.3f);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("地面に当たったぁ！");

            // プレイヤーにHPスクリプトがあれば呼び出す
            //PlayerHealth hp = other.GetComponent<PlayerHealth>();
            //if (hp != null)
            //{
            //    hp.TakeDamage(damage);
            //}

            // こぶしを消す
            Destroy(gameObject, 0.3f);
        }
    }
}
