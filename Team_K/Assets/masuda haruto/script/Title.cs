using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        
        PlayerPrefs.DeleteAll();//•Û‘¶ƒf[ƒg‚ğíœ
        PlayerPrefs.Save();

        PlayerData.Instance.ResetData();
    }
    public void onClickStartButton()
    {
        SceneManager.LoadScene("StageSelect");
    }

}
