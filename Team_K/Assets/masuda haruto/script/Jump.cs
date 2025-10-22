using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        JumpUpdate();
    }
    /// <summary>
	/// Update????ｪ%ｧ?????W?????v???????
	/// </summary>
	private void JumpUpdate()
    {
        // ?W?????v????
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {// ?W?????v?J?n
         // ?W?????v????v?Z
            float jumpPower = 50.0f;
            // ?W?????v???K?p
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GetComponent<Rigidbody2D>().linearVelocity.x, jumpPower);
        }
    }


}