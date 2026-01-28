using UnityEngine;

public class Tutorial : MonoBehaviour
{
    //エンターが押されたらシーン移行する
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            FadeManager.Instance.LoadScene("StageSelect", 1.0f);
        }
    }
}
