using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    [SerializeField]
    private string[] stageSceneNames = { "Stage1", "Stage2" };

    //ボタンを配列として定義
    [SerializeField]
    private
        Button[] _stageButton;

    [SerializeField]
    private
        GameObject[] _lockImages;//南京錠アイコン配列
    void Start()
    {
        //ステージのCLEAR数を取得
        int clearedStage = PlayerPrefs.GetInt("StageCleared", 0);

        //ステージボタンの表示・非表示
        for(int i=0;i < _stageButton.Length;i++)
        {

            int stageNumber = i + 1;

            bool unlocked = stageNumber <= clearedStage + 1;

            _stageButton[i].interactable = unlocked;

            if(_lockImages!=null&&i<_lockImages.Length)
            {
                _lockImages[i].SetActive(!unlocked);//反転して表示
            }
        }
    }

    public void StageSelect(int stageIndex)
    {

        if(stageIndex<0||stageIndex>=stageSceneNames.Length)
        {
            Debug.LogError("ボケカス。範囲外です。:" + stageIndex);
            return;
        }
        //受け取った因数(stage)のステージをロード
        FadeManager.Instance.LoadScene(stageSceneNames[stageIndex],0.5f);
    }
}
