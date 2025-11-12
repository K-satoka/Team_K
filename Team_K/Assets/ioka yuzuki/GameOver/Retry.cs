using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
   public void OnRetryButtonPressed()
    {
        //現在のシーンを読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
