using UnityEngine;

public class icicle_point : MonoBehaviour
{
    public Transform Player;//プレイヤーの位置特定

    void Update()
    {
        transform.position = new Vector2(Player.position.x, 100);
    }
}
