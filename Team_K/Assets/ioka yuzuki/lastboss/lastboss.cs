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

    public float firespeed = 10;
    public float fireInterval = 1.5f;
    public GameObject firePrefab;
    public Transform firepoint;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=fireInterval)
        {
            fire();
            timer = 0;
        }
    }

    void fire()
    {
        if (firepoint == null)
        {
            Debug.LogError("FirePoint がシーン上で null です！ どの lastboss が null か確認してください。", this);
            return;
        }
        GameObject fire = Instantiate(firePrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rbody=fire.GetComponent<Rigidbody2D>();
        rbody.linearVelocity = firepoint.right * firespeed;

    }
    void thunder()
    {



    }

    void Teleport()
    {
      


    }
}
