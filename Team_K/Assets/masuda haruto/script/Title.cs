using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void onClickStartButton()
    {
        SceneManager.LoadScene("Stage");
    }

}
