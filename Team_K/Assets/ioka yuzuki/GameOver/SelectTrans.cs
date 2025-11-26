using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectTrans : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip GameOverSE;
    public void LoadScene(string StageSelect)
    {
        if(AudioSource != null&&GameOverSE!=null)
        {
            AudioSource.PlayOneShot(GameOverSE);
        }

        FadeManager.Instance.LoadScene(StageSelect,1.0f);
    }
}
