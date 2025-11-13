using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CardType { HP,Attack }
public class Card : MonoBehaviour
{
    public CardType cardType;
    public TMP_Text cardText;//textMeshProならTMP_Text
    public int value;//上昇値

    private PlayerHP playerHP;
    private AttackCollision attackCollision;
    private void Start()
    {
        playerHP = FindObjectOfType<PlayerHP>();
        attackCollision = FindObjectOfType<AttackCollision>();

        //ボタンがあればクリック登録
        Button btn=GetComponent<Button>();
        if (btn != null )
            btn.onClick.AddListener(Onselect);

        UpdateText();
    }

    public void Setup(CardType type ,int randomValue)
    {
        cardType = type;
        value = randomValue;
        UpdateText();
    }

    void UpdateText()
    {
        if (cardText == null) return;
        
            switch(cardType)
            {
                case CardType.HP:
                    cardText.text = $"HPカード\n+{value}";
                    break;

                case CardType.Attack:
                    cardText.text = $"攻撃カード\n+{value}";
                    break;
                    
            }

        

    }

    public void Onselect()
    {
        if (cardType == CardType.HP&&playerHP!=null)
        {
            playerHP.IncreaseMaxHP(value);
        }
        else if (cardType == CardType.Attack&&attackCollision!=null)
        {
           attackCollision.IncreaseAttack(value);
        }

        Debug.Log($"{cardType}カードを選択。効果は:{value}");
    }
}
