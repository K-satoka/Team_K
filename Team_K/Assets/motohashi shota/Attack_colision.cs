using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private Collider2D[] childColliders; // �q�̓����蔻���ێ�
    private Collider2D selfCollider;     // �e���g�̓����蔻���ێ��i�c���p�j

    private void Awake()
    {
        // �e���g�� Collider2D ���擾
        selfCollider = GetComponent<Collider2D>();

        // �q�I�u�W�F�N�g�ɂ���S�Ă� Collider2D ���擾�i��A�N�e�B�u���܂ށj
        childColliders = GetComponentsInChildren<Collider2D>(includeInactive: true);

        // �������g�����O���āA������Ԃł͎q�̓����蔻���OFF��
        foreach (var col in childColliders)
        {
            if (col != selfCollider)
                col.enabled = false;
        }
    }

    /// <summary>
    /// �q�̓����蔻���ON�ɂ���
    /// </summary>
    public void EnableAttack()
    {
        foreach (var col in childColliders)
        {
            if (col != selfCollider)
                col.enabled = true;
        }
    }

    /// <summary>
    /// �q�̓����蔻���OFF�ɂ���
    /// </summary>
    public void DisableAttack()
    {
        foreach (var col in childColliders)
        {
            if (col != selfCollider)
                col.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("�G�Ƀq�b�g�I");
            other.GetComponent<EnemyHp>()?.TakeDamage(10);
        }
    }
}