//using JetBrains.Annotations;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Player_Move_Transition : MonoBehaviour
//{
//    // Update is called once per frame
//    void Update()
//    {
//        //GetComponentを用いてAnimatorコンポーネントを取り出す
//        Animator animator = GetComponent<Animator>();

//        //あらかじめ設定していたintパラメータ「Player_move_trans」の値を取り出す
//        int Player_move_trans = animator.GetInteger("Player_move_trans");
       
//        //左右の矢印キーを押した際にパラメータ「Player_move_trans」の値を増加させる
//        if (Input.GetKeyDown(KeyCode.RightArrow))
//        {
//            Player_move_trans++;
//            if (Player_move_trans >= 2)
//            {
//                Player_move_trans--;
//            }

//            Debug.Log(Player_move_trans);
//        }
//        if (Input.GetKeyDown(KeyCode.LeftArrow))
//        {
//            Player_move_trans++;
//            if (Player_move_trans >= 2)
//            {
//                Player_move_trans--;
//            }

//            Debug.Log(Player_move_trans);
//        }
//        //intパラメーターの値を設定する.
//        animator.SetInteger("Player_move_trans", Player_move_trans);

//    }
//}
