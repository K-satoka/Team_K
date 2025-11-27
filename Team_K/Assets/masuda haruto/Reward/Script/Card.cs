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


    private void Start()
    {
        playerHP = FindObjectOfType<PlayerHP>();
        attackCollision = FindObjectOfType<AttackCollision>();

        int stageUnlock = PlayerPrefs.GetInt("StageUnlock", 1);

        //ステージCLEAR数に応じて最大値を増やす

        maxValue += 3 * (stageUnlock - 1);

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

    public void Onselect()
    {

        //二回選択できないように
        Card[] cards = FindObjectsOfType<Card>();
        
        foreach (var c in cards)
        {
            if (c.button != null)
                c.button.interactable = false;
        }

        if(button != null)
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
}
