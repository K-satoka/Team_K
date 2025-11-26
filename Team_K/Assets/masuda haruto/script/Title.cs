using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip StartSE;
    private void Start()
    {
        Application.targetFrameRate = 60;
        
        PlayerPrefs.DeleteAll();//ï€ë∂ÉfÅ[ÉgÇçÌèú
        PlayerPrefs.Save();

        PlayerData.Instance.ResetData();
    }
    public void onClickStartButton()
    {
        if(audioSource != null&&StartSE)
        {
            audioSource.PlayOneShot(StartSE);
        }
        FadeManager.Instance.LoadScene("StageSelect",1.0f);
    }

}
