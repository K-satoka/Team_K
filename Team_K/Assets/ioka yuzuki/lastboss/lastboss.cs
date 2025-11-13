using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class lastboss : MonoBehaviour
{
    Rigidbody2D rbody;
    public float lastboss_atk;
    public float lastboss_max_hp;       //ボスの最大HP
    public float lastboss_current_hp;   //ボスの現在HP

    public float teleportInterval = 3f; // 何秒ごとにテレポートするか
    public Vector2 teleportRange = new Vector2(100f, 50f); // テレポート範囲

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Teleport()
    {
      


    }
}
