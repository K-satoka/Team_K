using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectTrans : MonoBehaviour
{
    public void LoadScene(string StageSelect)
    {
        FadeManager.Instance.LoadScene(StageSelect,1.0f);
    }
}
