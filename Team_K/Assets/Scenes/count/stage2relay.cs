using UnityEngine;

public class stage2relay : MonoBehaviour
{
    EnemyHp enemyhp;
    public int killcnt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyhp = GetComponent<EnemyHp>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyhp.Enemy_Current_Hp <= 0)
            killcnt++;
    }
}
