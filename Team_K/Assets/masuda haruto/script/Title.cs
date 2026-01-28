using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip StartSE;

    public Button startButton;
    private void Start()
    {
        Application.targetFrameRate = 60;
        
        PlayerPrefs.DeleteAll();//保存データを削除
        PlayerPrefs.Save();

        PlayerData.Instance.ResetData();

        EventSystem.current.SetSelectedGameObject(startButton.gameObject);
    }

    //クリック時にシーン移行
    public void onClickStartButton()
    {
        if(audioSource != null&&StartSE)
        {
            audioSource.PlayOneShot(StartSE);
        }
        FadeManager.Instance.LoadScene("Tutorial",1.0f);
    }

}
