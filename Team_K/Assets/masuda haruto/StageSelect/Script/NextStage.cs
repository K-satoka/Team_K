using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    //ボタンを配列として定義
    [SerializeField]
    private
        Button[] _stageButton;
    void Start()
    {
        //ステージのCLEAR数を取得
        int stageUnlock = PlayerPrefs.GetInt("StageUnlock", 1);

        //ステージボタンの表示・非表示
        for(int i=0;i < _stageButton.Length;i++)
        {
            if (i < stageUnlock)
            {
                _stageButton[i].interactable = true;
            }
            else
                _stageButton[i].interactable = false;
        }
    }

    public void StageSelect(int Stage)
    {
        //受け取った因数(stage)のステージをロード
        SceneManager.LoadScene(Stage);
    }
}
