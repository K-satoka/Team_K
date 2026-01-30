using UnityEngine;
using UnityEngine.Audio;

public class Tutorial : MonoBehaviour
{

    //SE
    public AudioSource audioSource;
    public AudioClip TutorialSE;
    //エンターが押されたらシーン移行する
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (audioSource != null && TutorialSE)
                audioSource.PlayOneShot(TutorialSE);
            
            FadeManager.Instance.LoadScene("StageSelect", 1.0f);
        }
    }
}
