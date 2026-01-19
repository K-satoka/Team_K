using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum CardType { HP,Attack }
public class Card : MonoBehaviour
{
    public CardType cardType;
    public TMP_Text cardText;//textMeshProならTMP_Text
    public int value;//上昇値

    [Header("ランダムの範囲")]
    public int minValue = 1;
    public int maxValue = 3;

    private PlayerHP playerHP;
    private AttackCollision attackCollision;

    //SE
    public AudioSource AudioSource;
    public AudioClip CardSelectSE;

    public Button button;

    //
    private bool inputLocked = false;
    private CanvasGroup canvasGroup;


    private void Start()
    {

        //canvasgroupの初期化
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null )
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        inputLocked = false;//シーン開始の解除
        UpdateBottunVidual();

        playerHP = FindObjectOfType<PlayerHP>();
        attackCollision = FindObjectOfType<AttackCollision>();

        //現在のステージ番号を取得
        EnemyHp enemy = FindObjectOfType<EnemyHp>();
        Debug.Log("読み込み LastClearedStage = " +
    PlayerPrefs.GetInt("LastClearedStage", -1));

        int lastStage = PlayerPrefs.GetInt("LastClearedStage", 1) - 1;

        if(lastStage < 1 )lastStage = 1;

        Debug.Log("CurrentStage(from EnemyHp)=" + lastStage);

        //ステージCLEAR数に応じて最大値を増やす

        maxValue = maxValue * (int)Mathf.Pow(2, lastStage - 1);

        Debug.Log($"ステージ{lastStage}に応じてカードの値を設定: minValue={minValue}, maxValue={maxValue}");

        //ボタンがあればクリック登録
        Button btn=GetComponentInChildren<Button>();
        if (btn != null )
            btn.onClick.AddListener(Onselect);



        //ランダム決定
        GenerateRandomValue();

        UpdateText();

        Debug.Log($"MinValue={minValue} / MaxValue={maxValue}");
    }

    public void Setup(CardType type ,int randomValue)
    {
        cardType = type;
        value = randomValue;
        UpdateText();
    }

    //
    //カードの種類をセットしてランダム値を生成
    //
    public void Setup(CardType type)
    {
        cardType = type;
        GenerateRandomValue();
        UpdateText();
    }
    //
    //ランダム値を生成
    //
    void GenerateRandomValue()
    {
        value = Random.Range(minValue, maxValue+1);
    }

    void UpdateText()
    {
        if (cardText == null) return;
        
            switch(cardType)
            {
                case CardType.HP:
                    cardText.text = $"HP+{value}";
                    break;

                case CardType.Attack:
                    cardText.text = $"Attack+{value}";
                    break;
                    
            }

        

    }

    private void UpdateBottunVidual()
    {
        if (canvasGroup != null)
            canvasGroup.alpha = inputLocked ? 0.5f : 1.0f;//押せない間は半透明
        if (button != null)
            button.interactable = !inputLocked;
    }

    public void Onselect()
    {
        //フェード中(=ロック中)は虫
        if(inputLocked) return;
        //フェード開始と同時にロック
        inputLocked = true;
        UpdateBottunVidual();

       
        //二回選択できないように
        Card[] cards = FindObjectsOfType<Card>();
        
        foreach (var c in cards)
        {
            c.inputLocked = true;
            c.UpdateBottunVidual();
            //if (c.button != null)
            //    c.button.interactable = false;
        }

        float fadeTime = 1.0f;
        StartCoroutine(UnlockInputApprox(fadeTime));


        if (button != null)
            button.interactable = false;
        //SE
        if(AudioSource != null&&CardSelectSE!=null)
        {
            AudioSource.PlayOneShot(CardSelectSE);
        }

        //各カードの結果の反映とログ
        if (cardType == CardType.HP)
        {
            PlayerData.Instance.maxHP_Up += value;
        }
        else if(cardType==CardType.Attack)
        {
            PlayerData.Instance.attack_up += value;
        }

            Debug.Log($"{cardType}カードを選択。効果は:{value}");

        //ステージセレクトに飛ぶ
        FadeManager.Instance.LoadScene("StageSelect",1.0f);
    }

    private IEnumerator UnlockInputApprox(float fadeDuraction)
    {
        yield return new WaitForSeconds(fadeDuraction * 2f); // フェードアウト＋フェードインを待つ

        // 全カードの操作ロック解除と見た目更新
        Card[] cards = FindObjectsOfType<Card>();
        foreach (var c in cards)
        {
            c.inputLocked = false;
            c.UpdateBottunVidual();
        }
    }
}
