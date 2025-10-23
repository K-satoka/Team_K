using UnityEngine;
public class Enemy : MonoBehaviour
{
    public int hp = 50;


    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"ダメージ: {damage}, 残りHP: {hp}");

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("敵が倒れた！");
        Destroy(gameObject);
    }
}

