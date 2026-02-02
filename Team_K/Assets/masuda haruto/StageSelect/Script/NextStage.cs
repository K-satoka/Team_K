using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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

    [SerializeField]
    private Button MenuButton;

    public AudioSource audioSource;
    public AudioClip selectSE;

    private bool isLoading = false;

    private int currentIndex = 0;//選択中のステージ
    private int stageUnlock;

    [SerializeField]
    private Vector2 nomalScale = new Vector2(1,1);

    [SerializeField]
    private Vector2 selectedScale = new Vector2(2,2);


    void Start()
    {
        //ステージのCLEAR数を取得
        stageUnlock = PlayerPrefs.GetInt("StageUnlock", 1);

        currentIndex = PlayerPrefs.GetInt("SelectedStageIndex", 0);
        Debug.Log("復元したIndex: " + currentIndex);

        //ステージボタンの表示・非表示
        for (int i=0;i < _stageButton.Length;i++)
        {
            bool unlocked = (i+1) <= stageUnlock;

            _stageButton[i].interactable = unlocked;
            if(_lockImages!=null&&i<_lockImages.Length)
            {
                _lockImages[i].SetActive(!unlocked);//反転して表示
            }

            RectTransform rt=_stageButton[i].GetComponent<RectTransform>();
            rt.sizeDelta = nomalScale;
        }

        SetSelectButton(currentIndex);
    }
    void Update()
    {
        if (isLoading) return;

        // 右キー
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveSelect(1);
        }

        // 左キー
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveSelect(-1);
        }
        //上キー
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectMenuButton();
        }
        //下キー
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ReturnToStageButton();
        }

        // 決定（Enter or Space）
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            GameObject current = EventSystem.current.currentSelectedGameObject;

            if(current==MenuButton.gameObject)
            {
                OpenMenu();
                return;
            }

            if (current == _stageButton[currentIndex].interactable
                && _stageButton[currentIndex].interactable)
            {
                StageSelect(currentIndex + 1);
            }
        }
    }
    //キーボード選択用
    void MoveSelect(int direction)
    {
        int nextIndex = currentIndex;

        do
        {
            nextIndex += direction;

            if (nextIndex < 0)
                nextIndex = _stageButton.Length - 1;
            else if (nextIndex >= _stageButton.Length)
                nextIndex = 0;

        } while (!_stageButton[nextIndex].interactable);

        currentIndex = nextIndex;
        SetSelectButton(currentIndex);
    }

    void SetSelectButton(int index)
    {
        StopAllCoroutines();
        for (int i = 0; i < _stageButton.Length; i++)
        {
           RectTransform rt = _stageButton[i].GetComponent<RectTransform>();
            StartCoroutine(ResizeButton(rt, nomalScale));
        }

        RectTransform selectRT = _stageButton[index].GetComponent<RectTransform>();
        StartCoroutine(ResizeButton(selectRT, selectedScale));

        _stageButton[index].Select();
    }
    //選択されたステージの大きさをちょっと大きくする
    IEnumerator ResizeButton(RectTransform target, Vector2 targetSize)
    {
        float time = 0f;
        float duration = 0.1f;

        Vector2 start = target.sizeDelta;
        Vector2 end = targetSize;

        while (time < duration)
        {
            time += Time.deltaTime;
            target.sizeDelta = Vector2.Lerp(start, end, time / duration);
            yield return null;
        }

        target.sizeDelta = end;
    }
    //ステージセレクト
    public void StageSelect(int StageNumber)
    {
        if (isLoading) return;
        Debug.Log("StageSelect 呼ばれた / index = " + currentIndex);
        isLoading = true;
        //選択されたステージの情報保存
        PlayerPrefs.SetInt("SelectedStageIndex", currentIndex);
        PlayerPrefs.Save();
       

        //一度押されたら全部のボタンを無効化
        foreach (Button btn in _stageButton)
        {
            btn.interactable = false;
        }
        //効果音を鳴らす
        if (audioSource != null && selectSE != null)
        {
            audioSource.PlayOneShot(selectSE);
          
        }

        string scaneName = "Stage" + StageNumber;

        //受け取った因数(stage)のステージをロード
        FadeManager.Instance.LoadScene(scaneName,1.0f);
    }

    void SelectMenuButton()
    {
        StopAllCoroutines();

        for (int i = 0; i < _stageButton.Length; i++)
        {
            RectTransform rt = _stageButton[i].GetComponent<RectTransform>();
            StartCoroutine(ResizeButton(rt, nomalScale));
        }
        MenuButton.Select();

    }

    void ReturnToStageButton()
    {
        StopAllCoroutines();

        for (int i = 0; i < _stageButton.Length; i++)
        {
            RectTransform rt=_stageButton[i].GetComponent<RectTransform>();
            StartCoroutine(ResizeButton(rt, nomalScale));
        }

        RectTransform selectRT = _stageButton[currentIndex].GetComponent<RectTransform>();
        StartCoroutine(ResizeButton(selectRT,selectedScale));

        _stageButton[currentIndex].Select();
    }
    void OpenMenu()
    {
        Debug.Log("メニューを開く");

        // 例：
        // menuPanel.SetActive(true);
        // SE鳴らす
        if (audioSource != null && selectSE != null)
        {
            audioSource.PlayOneShot(selectSE);
        }
    }

}
