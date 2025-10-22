using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public int Enemy_MAX_Hp = 10;//最大hp
    public int Enemy_Current_Hp;//現在のhp

    public int damageOnContact = 10;
    void Start()
    {
       Enemy_Current_Hp = Enemy_MAX_Hp;   //初期値を最大値に設定
    }

    public void TakeDamage(int damage)
    {
       Enemy_Current_Hp -= damage;
        Debug.Log("Enemy took" + damage + " Enemy_Current_Hp");

        if ( Enemy_Current_Hp < 0)
        {
            Die();
        }
    }

void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);//ゲームオブジェクトを削除
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
            {
            Debug.Log("Enemy touched the Player!");

            TakeDamage(damageOnContact);
        }
    }
}
