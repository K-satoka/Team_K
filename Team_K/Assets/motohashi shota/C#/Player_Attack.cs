using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player_Attack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int attackDamage = 20;
    public float attackRate = 1f;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    private float nextAttackTime = 0f;

    public AudioSource AudioSource;
    public AudioClip AttackSE;
    public AttackCollision attackCollision;

    private Coroutine atttackCorotine;
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Keyboard.current.zKey.wasPressedThisFrame)
            {
                DoAttack();
            }
        }
    }

    void DoAttack()
    {
        nextAttackTime = Time.time + 1f / attackRate;

        if (AudioSource != null && AttackSE != null)
        {
            AudioSource.PlayOneShot(AttackSE);
        }

        // AttackCollision のコライダーをON
        //if (attackCollision != null)
        //{
        //    attackCollision.EnableAttack();
        //    StartCoroutine(DisableAttackAfterTime(0.5f)); // 攻撃判定の持続時間（アニメに合わせる）
        //}

        // 当たり判定だけここで実施
        // 攻撃判定ON
        if (attackCollision != null)
        {
            attackCollision.EnableAttack();
            //StartCoroutine(DisableAttackAfterTime(0.5f)); // 攻撃アニメーションに合わせて判定時間を調整
        

            //前のコルーチンが動いてたら止める
            if (atttackCorotine != null)
                StopCoroutine(atttackCorotine);

            //新しいコルーチンを開始
            atttackCorotine = StartCoroutine(DisableAttackAfterTime(0.5f));
        }
    }
    private IEnumerator DisableAttackAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (attackCollision != null)
        {
            attackCollision.DisableAttack();
        }

        atttackCorotine = null;//終わったらクリア
    }
}
