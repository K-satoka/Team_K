using UnityEngine;

public class Player_Defense : MonoBehaviour
{
    [Header("Defense Settings")]
    public float damageReduction = 0.5f;  // ダメージ減少率（例：0.5f で50%減少)
    private bool isDefending = false;  // 防御中かどうか
    private float currentHealth = 100f;  // 仮HP、統合するときになったらコメントアウトするか消してねー
    void Update()
    {
        // 防御ボタンが押された時、防御開始
        if (Input.GetKey(KeyCode.DownArrow))
        {
            StartDefense();
        }
        // 防御解除
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            StopDefense();
        }
    }

    // 防御を開始する
    void StartDefense()
    {
        isDefending = true;
    }

    // 防御を解除する
    void StopDefense()
    {
        isDefending = false;
    }

    // ダメージを受けた時に防御中ならダメージを軽減
    public void TakeDamage(float damage)
    {
        if (isDefending)
        {
            damage *= (1f - damageReduction);  // ダメージを軽減
        }

        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);
    }
}
