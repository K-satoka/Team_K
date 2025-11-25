using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenu : MonoBehaviour
{
    // Inspector から設定可能なシーン名
    [SerializeField] private string sceneName = "StatusMenu";

    // ボタンに設定する関数
    public void OnButtonPressed()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            // シーンを読み込む
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("シーン名が設定されていません。Inspectorで設定してください。");
        }
    }
    public void Click()
    {
        Debug.Log("ボタンが押されました！");
    }
}
