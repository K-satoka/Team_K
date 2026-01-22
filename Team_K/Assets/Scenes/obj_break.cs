using UnityEngine;

public class obj_break : MonoBehaviour
{
    private EnemyHp enemyhp;
    float objbreak;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyhp = GetComponent<EnemyHp>();
    }

    // Update is called once per frame
    void Update()
    {
        objbreak = enemyhp.Enemy_Current_Hp;
        if (objbreak <= 0)
        {
            Destroy(gameObject);
        }
    }
}
