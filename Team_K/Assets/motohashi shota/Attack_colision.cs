using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private Collider2D attackCollider;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        if (attackCollider == null)
            Debug.LogError("���̃I�u�W�F�N�g�� Collider2D ��������܂���I");
        else
            attackCollider.enabled = false; // ������OFF
    }

    public void EnableAttack() => attackCollider.enabled = true;
    public void DisableAttack() => attackCollider.enabled = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("�G�Ƀq�b�g�I");
            other.GetComponent<Enemy>()?.TakeDamage(10);
        }
    }
}