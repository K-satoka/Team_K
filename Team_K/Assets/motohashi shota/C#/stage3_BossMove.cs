using UnityEngine;

public class stage3_BossMove : MonoBehaviour
{
    [Header("�^�[�Q�b�g")]
    public Transform player;

    [Header("�ړ��p�����[�^")]
    //�ړ����x
    public float moveSpeed = 3f;
    //�~�܂�Ƃ��̊�
    public float stopDistance = 2f;

    //Rigidbody�A�A�j���[�^�[�A�X�v���C�g�����_���[�p
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
    {
        //�A�j���[�^�[�ARiGidBody�擾
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        //�v���C���[���Ȃ��ƒl��Ԃ��ˁ[
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < stopDistance)
        {
            //�߂Â���������~�܂�ˁ[
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetBool("isMoving", false);
        }
        else
        {
            // �v���C���[��ǂ�������i���ړ��̂݁j
            float dirX = player.position.x - transform.position.x; // ���E�̍������߂�
            if (dirX != 0)
            {
                sr.flipX = dirX > 0;//�v���C���[�̕��ɍs��
            }

            dirX = Mathf.Sign(dirX); // ���E�̌������� -1 or 1 �ɂ���(matif.sign�Ő����O�Ȃ�P���A���Ȃ�-1��Ԃ�)

            rb.linearVelocity = new Vector2(dirX * moveSpeed, rb.linearVelocity.y);
            anim.SetBool("isMoving", true);
        }
    }
}
