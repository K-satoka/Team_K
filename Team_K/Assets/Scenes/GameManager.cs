using UnityEngine;

public class GameManager : MonoBehaviour
{
    // この変数は「このシーンのGameManager」が持つカウント
    public static int st1_totalKills = 0;
    public int cnt;
    // 敵が死んだときに呼ぶメソッド
    public void EnemyDefeated()
    {
        st1_totalKills++;
        cnt = st1_totalKills;
        Debug.Log("このステージの討伐数: " + st1_totalKills);
    }
}
