using System.Globalization;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class stage1killcnt : MonoBehaviour
{
    public TextMeshProUGUI kill_cnt;

    stage1relay kill;
    int cnt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       kill_cnt = GetComponent<TextMeshProUGUI>();
        
       kill = GameObject.Find("Image_player").GetComponent<stage1relay>();
    }

    // Update is called once per frame
    void Update()
    {
        
        kill_cnt.text = "cnt " + cnt;
    }
}
