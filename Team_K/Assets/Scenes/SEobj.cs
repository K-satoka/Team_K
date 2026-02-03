using UnityEngine;
using UnityEngine.Audio;

public class SEobj : MonoBehaviour
{
      public AudioSource audioSource;
    public AudioClip EnemyDieSE;

    void Start()
    {
        EnemyHp enemyhp = GameObject.Find("stage2_Boss").GetComponent<EnemyHp>();

        enemyhp.OnDie += PlaySE;
    }

    void PlaySE()
    {
        audioSource.PlayOneShot(EnemyDieSE);
    }
}
