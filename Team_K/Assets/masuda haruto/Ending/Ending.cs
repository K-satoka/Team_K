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
    public AudioClip ResetSE;
    // ボタンを押したときに呼ぶ
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }

    public void OnClickButton()
    {
        if (audioSource != null && ResetSE != null)
            audioSource.PlayOneShot(ResetSE);
       

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
        if (audioSource != null && EndingSE != null)
            audioSource.PlayOneShot(EndingSE);
        FadeManager.Instance.LoadScene("Title", 1.0f);
    }

    // キャンセルボタン
    
    private IEnumerator SelectOKNextFrame()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(okButton.gameObject);
    }
}
