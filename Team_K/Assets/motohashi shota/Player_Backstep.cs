using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Backstep : MonoBehaviour
{
    [Header("�o�b�N�X�e�b�v�ݒ�")]
    public float stepSpeed = 10f;            // �o�b�N�X�e�b�v���x
    public float stepDuration = 0.15f;       // �o�b�N�X�e�b�v����
    public float stepCooldown = 1f;          // �N�[���^�C��

    private bool isStepping = false;         // �X�e�b�v�����ǂ���
    private bool isCooldown = false;         // �N�[���^�C�������ǂ���
    private Vector2 stepDirection;           // �X�e�b�v����

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // LeftShift�L�[�ŉ��
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame && !isStepping && !isCooldown)
        {
            StartBackstep();
        }
    }

    //==============================
    // �o�b�N�X�e�b�v�J�n
    //==============================
    void StartBackstep()
    {
        isStepping = true;
        isCooldown = true;

        // �v���C���[�̌����Ă�������̔��΂ֈړ�
        float facing = Mathf.Sign(transform.localScale.x);
        stepDirection = new Vector2(facing, 0f);

        // �� �������u�Ԃɑ��x��^���ē�����
        rb.linearVelocity = stepDirection * stepSpeed;

        // �� �A�j���[�V�����J�n�iTrigger�����j
        animator?.SetTrigger("Backstep");

        // ���G���C���[�ɕύX
        gameObject.layer = LayerMask.NameToLayer("PlayerInvincible");

        // �N�[���^�C�������\��
        CancelInvoke("ResetCooldown");
        Invoke("ResetCooldown", stepCooldown);

        // �� �o�b�N�X�e�b�v�I���\��i�A�j�����Ԃƍ��킹��j
        Invoke("EndBackstep", stepDuration);
    }

    //==============================
    // �o�b�N�X�e�b�v�I��
    //==============================
    void EndBackstep()
    {
        isStepping = false;

        // �ړ����~�߂�
        rb.linearVelocity = Vector2.zero;

        // ���G����
        gameObject.layer = LayerMask.NameToLayer("");

        // �f�o�b�O�m�F�p
        // Debug.Log("�o�b�N�X�e�b�v�I��");
    }

    //==============================
    // �N�[���^�C������
    //==============================
    void ResetCooldown()
    {
        isCooldown = false;
    }
}
