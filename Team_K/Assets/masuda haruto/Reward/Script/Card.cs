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
    public int maxValue = 5;

    private PlayerHP playerHP;
    private AttackCollision attackCollision;
    private void Start()
    {
        playerHP = FindObjectOfType<PlayerHP>();
        attackCollision = FindObjectOfType<AttackCollision>();

        //ステージに合わせて増加
        SetvaluerageByStage();

        //ボタンがあればクリック登録
        Button btn=GetComponentInChildren<Button>();
        if (btn != null )
            btn.onClick.AddListener(Onselect);

        //ランダム決定
        GenerateRandomValue();

        UpdateText();
    }

    //ステージに合わせてふり幅増幅
    void SetvaluerageByStage()
    {
        int clearedNumber = PlayerPrefs.GetInt("StageCleared", 0);

        //例:ステージが進むほ最大数が増える
        minValue = 1 + clearedNumber;

         maxValue = minValue + 5 + clearedNumber*2;
    
        Debug.Log($"[Card]stage:{clearedNumber},maxValue:{minValue},max:{maxValue}");
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
