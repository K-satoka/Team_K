using System.Globalization;
using UnityEngine;

public class stage1killcnt : MonoBehaviour
{
    stage1relay kill;
    private int cnt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       kill = GetComponent<stage1relay>();
        
    }

    // Update is called once per frame
    void Update()
    {
        cnt = kill.killcnt;
    }
}
