using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectTrans : MonoBehaviour
{
    public void LoadScene(string StageSelect)
    {
        SceneManager.LoadScene(StageSelect);
    }
}
