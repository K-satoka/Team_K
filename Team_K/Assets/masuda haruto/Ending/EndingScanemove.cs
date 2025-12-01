using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class EndingScanemove : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip StageselectSE;

    public void onClickButton()
    {
        if (audioSource != null && StageselectSE != null)
            audioSource.PlayOneShot(StageselectSE);
        FadeManager.Instance.LoadScene("StageSelect", 1.0f);
    }

}
