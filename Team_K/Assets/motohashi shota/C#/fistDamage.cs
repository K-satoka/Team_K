using UnityEngine;

public class fistDamage : MonoBehaviour
{
    public int damage = 1; // ダメージ量

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("こぶしヒット！");

            // プレイヤーにHPスクリプトがあれば呼び出す
            //PlayerHealth hp = other.GetComponent<PlayerHealth>();
            //if (hp != null)
            //{
            //    hp.TakeDamage(damage);
            //}

            // こぶしを消す
            Destroy(gameObject);
        }
    }
}
