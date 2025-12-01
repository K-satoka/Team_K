using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScanemove : MonoBehaviour
{


    public void onClickButton()
    {
       
        FadeManager.Instance.LoadScene("StageSelect", 1.0f);
    }

}
