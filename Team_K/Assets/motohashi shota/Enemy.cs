using UnityEngine;
public class Enemy : MonoBehaviour
{
    public int hp = 50;


    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"�_���[�W: {damage}, �c��HP: {hp}");

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("�G���|�ꂽ�I");
        Destroy(gameObject);
    }
}

