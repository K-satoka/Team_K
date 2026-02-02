using UnityEngine;

public class stage5killcnt : MonoBehaviour
{
    stage5relay kill;
    private int cnt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        kill = GetComponent<stage5relay>();

    }

    // Update is called once per frame
    void Update()
    {
        cnt = kill.killcnt;
    }
}
