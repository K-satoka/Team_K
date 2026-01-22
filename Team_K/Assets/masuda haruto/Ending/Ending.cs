using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel;
    [SerializeField] private Button okButton;

    [SerializeField] private CanvasGroup mainCanvasGroup;
    [SerializeField] private CanvasGroup confirmCanvasGroup;

    public Button firstButton;

    public AudioSource audioSource;
    public AudioClip EndingSE;
    // ボタンを押したときに呼ぶ
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }

    public void OnClickButton()
    {
        confirmPanel.SetActive(true);   // 確認パネルを表示
      

        //裏側の入力を停止
        mainCanvasGroup.interactable = false;
        mainCanvasGroup.blocksRaycasts = false;


        //確認用のパネルを有効か
        confirmCanvasGroup.interactable = true;
        confirmCanvasGroup.blocksRaycasts = true;

        StartCoroutine(SelectOKNextFrame());
    }

    // OKボタン
    public void OnClickOK()
    {
        //CloseConfirm();
        FadeManager.Instance.LoadScene("Title", 1.0f);
    }

    // キャンセルボタン
    public void OnClickCancel()
    {
        CloseConfirm();
    }

    void CloseConfirm()
    {
        confirmPanel.SetActive(false);

        //裏側の復活
        mainCanvasGroup.interactable = true;
        mainCanvasGroup.blocksRaycasts = true;

        confirmCanvasGroup.interactable = false;
        confirmCanvasGroup.blocksRaycasts = false;

        if (audioSource != null && EndingSE != null)
            audioSource.PlayOneShot(EndingSE);

        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }
    
    private IEnumerator SelectOKNextFrame()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(okButton.gameObject);
    }
}
