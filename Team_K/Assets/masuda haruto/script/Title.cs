using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        
        PlayerPrefs.DeleteAll();//保存デートを削除
        PlayerPrefs.Save();
    }
    public void onClickStartButton()
    {
        SceneManager.LoadScene("StageSelect");
    }

}
