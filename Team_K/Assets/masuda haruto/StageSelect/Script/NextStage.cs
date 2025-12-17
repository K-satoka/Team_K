using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class NextStage : MonoBehaviour
{
    //ボタンを配列として定義
    [SerializeField]
    private
        Button[] _stageButton;

    [SerializeField]
    private
        GameObject[] _lockImages;//南京錠アイコン配列

    public AudioSource audioSource;
    public AudioClip selectSE;

    private bool isLoading = false;
    

    void Start()
    {
        //ステージのCLEAR数を取得
        int stageUnlock = PlayerPrefs.GetInt("StageUnlock", 1);

        //ステージボタンの表示・非表示
        for(int i=0;i < _stageButton.Length;i++)
        {
            bool unlocked = (i+1) <= stageUnlock;

            _stageButton[i].interactable = unlocked;
            if(_lockImages!=null&&i<_lockImages.Length)
            {
                _lockImages[i].SetActive(!unlocked);//反転して表示
            }
        }
    }

    public void StageSelect(int StageNumber)
    {
        if (isLoading) return;

        isLoading = true;

        //全部のボタンを無効化
        foreach (Button btn in _stageButton)
        {
            btn.interactable = false;
        }
        if (audioSource != null && selectSE != null)
        {
            audioSource.PlayOneShot(selectSE);
          
        }

        string scaneName = "Stage" + StageNumber;

        //受け取った因数(stage)のステージをロード
        FadeManager.Instance.LoadScene(scaneName,1.0f);
    }
}
