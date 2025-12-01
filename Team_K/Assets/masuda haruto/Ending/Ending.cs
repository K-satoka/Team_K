using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel;

    // ボタンを押したときに呼ぶ
    public void OnClickButton()
    {
        confirmPanel.SetActive(true);   // 確認パネルを表示
    }

    // OKボタン
    public void OnClickOK()
    {
        Debug.Log("実行しました");
        confirmPanel.SetActive(false);
        FadeManager.Instance.LoadScene("Title", 1.0f);
    }

    // キャンセルボタン
    public void OnClickCancel()
    {
        confirmPanel.SetActive(false);
    }
}
