using UnityEngine;

public class stage1relay : MonoBehaviour
{
    public static int killCount = 0;
    public int kiiiiii;
    void OnEnable()
    {
        EnemyHp.OnEnemyDead += AddCount;
    }

    void OnDisable()
    {
        EnemyHp.OnEnemyDead -= AddCount;
    }

    void AddCount()
    {
        killCount++;
        kiiiiii=killCount;
        Debug.Log("ƒJƒEƒ“ƒg‘ÎÛ‚Ì“GŒ‚”j”: " + killCount);
    }
}
