using UnityEngine;

public class Attack_colision : MonoBehaviour
{
    public Collider2D attackCollider;

    // �U������ON
    public void EnableAttack()
    {
        attackCollider.enabled = true;
    }

    // �U������OFF
    public void DisableAttack()
    {
        attackCollider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("�G�Ƀq�b�g�I");
            // �G�Ƀ_���[�W��^���鏈���������ɏ���
            other.GetComponent<Enemy>()?.TakeDamage(10);
        }
    }
}
