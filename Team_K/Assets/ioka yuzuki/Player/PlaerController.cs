using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaerController : MonoBehaviour
{
    Rigidbody2D rbody;  // Rigidbody2D�^�̕ϐ�
    float axisH = 0.0f; //����
    public float speed = 805.0f;

    public float jump = 9.0f;
    public LayerMask GroundLayer;
    bool goJump = false;
    bool onGround = false;
    bool isAttacking = false;

    public int Max_JumpCount = 2;        //�ő�W�����v��
    private int currentJumpCount = 0;

    public float knock_back_right;
    public float knock_back_left;

    //�A�j���[�V�����Ή�
    Animator animator;//�A�j���[�^�[
    public string waiting = "PlayerStop";
    public string PlayerMove = "PlayerMove";
    public string PlayerJump = "PlayerJump";
    public string PlayerAttack = "Player_Attack";
    string nowAnime = "";
    string oldAnime = "";

    void Start()
    {
        // Rigidbody2D���Ƃ��Ă���
        rbody = this.GetComponent<Rigidbody2D>();//Rigidbody2D����Ƃ��Ă���
        animator = GetComponent<Animator>();  //Animator���Ƃ��Ă���
        nowAnime = waiting;                   //��~����J�n
        oldAnime = waiting;                   //��~����J�n
    }

    // Update is called once per frame
    void Update()
    {
        // �U������
        if (!isAttacking && Input.GetKeyDown(KeyCode.Z))
        {
            isAttacking = true;
            animator.Play(PlayerAttack);           // �U���A�j���Đ�
            StartCoroutine(EndAttackAnimation());  // �U���I����ɑҋ@�ɖ߂�
        }

        //�����������`�F�b�N����
        axisH = Input.GetAxisRaw("Horizontal");
        if (axisH > 0.0f)
        {
            //�E�ړ�
            transform.localScale = new Vector2(-1, 1);
        }
        else if (axisH < 0.0f)
        {
            //���ړ�
            transform.localScale = new Vector2(1, 1);
        }

        //�L�����N�^�[���W�����v������
        if (Input.GetButtonDown("Jump"))
        {
            if (onGround || currentJumpCount < Max_JumpCount)
            {
                Jump(); //�W�����v����
            }
        }
    }

    // �U���A�j���I����Ƀt���O�����Z�b�g���đҋ@�ɖ߂�
    IEnumerator EndAttackAnimation()
    {
        yield return new WaitForSeconds(0.3f); // �U���A�j���̒����ɍ��킹��
        isAttacking = false;
        nowAnime = waiting;
        animator.Play(waiting);
    }

    void FixedUpdate()
    {
        //�n�㔻��
        onGround = Physics2D.CircleCast(transform.position,//���ˈʒu
            0.2f,            //�~�̔��a
            Vector2.down,    //���˕���
            0.0f,            //���ˋ���
            GroundLayer);    //���o���郌�C���[

        // �ړ�����
        if (onGround || axisH != 0)
        {
            //�n�ʂ̏�or���x��0�ł͂Ȃ� �� ���x���X�V
            rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);
        }

        if (onGround)
        {
            currentJumpCount = 0; //�W�����v�񐔃��Z�b�g
        }

        //�A�j���[�V�����X�V
        if (!isAttacking) //�U�����͑��̃A�j�����㏑�����Ȃ�
        {
            if (onGround)
            {
                //�n�ʂ̏�F�ړ�������~�����ŃA�j���؂�ւ�
                nowAnime = axisH != 0 ? PlayerMove : waiting;
            }
            else
            {
                //��
                nowAnime = PlayerJump;
            }

            //�O��̃A�j���ƈقȂ�ꍇ�̂ݍĐ�
            if (nowAnime != oldAnime)
            {
                oldAnime = nowAnime;
                animator.Play(nowAnime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // 攻撃元から自分への方向
            Vector2 knockDir = (transform.position - collision.transform.position).normalized;

            // 上方向も加える
            knockDir += Vector2.up * 0.5f;
            knockDir.Normalize();

            // 左右で別の力を設定
            float force = 1.5f;
            if (knockDir.x > 0)
            {
                // 左から攻撃された → 右に吹っ飛ぶ
                force = knock_back_right;
            }
            else
            {
                // 右から攻撃された → 左に吹っ飛ぶ
                force = knock_back_left;
            }

            rbody.AddForce(knockDir * force);

            Debug.Log("吹っ飛んだ！");
            //if (transform.localScale.x >= 0)
            //{
            //    //�E�����m�b�N�o�b�N
            //    Vector2 knockback = (transform.right * 1.3f + transform.up * 1.5f).normalized;
            //    this.rbody.AddForce(knockback * knock_back_left);
            //    Debug.Log("����");
            //}
            //else
            //{
            //    //�������m�b�N�o�b�N
            //    Vector2 knockback2 = (transform.right * -1.3f + transform.up * 1.5f).normalized;
            //    this.rbody.AddForce(knockback2 * knock_back_right);
            //}
        }
    }

    //�W�����v����
    public void Jump()
    {
        Vector2 jumpPw = new Vector2(0, jump);//�W�����v�p�x�N�g��
        rbody.AddForce(jumpPw, ForceMode2D.Impulse);
        goJump = false;
        currentJumpCount++;
    }
}