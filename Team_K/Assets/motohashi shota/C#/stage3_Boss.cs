using UnityEngine;

public class stage3_Boss : MonoBehaviour
{
    //Rigidbody用
    public Rigidbody rd;
    //アニメーション用
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
