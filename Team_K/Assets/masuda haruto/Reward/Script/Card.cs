using UnityEngine;
using UnityEngine.UI;

public enum CardType { HP,Attack }
public class Card : MonoBehaviour
{
    public CardType cardType;
    public Text cardText;//textMeshProÇ»ÇÁTMP_Text
    public int value;//è„è∏íl

    private PlayerHP playerHP;
    private AttackCollision attackCollision;
    private void Start()
    {
        playerHP = FindObjectOfType<PlayerHP>();
        attackCollision = FindObjectOfType<AttackCollision>();
    }

    public void Setup(CardType type ,int randomValue)
    {
        cardType = type;
        value = randomValue;
        UpdateText();
    }

    void UpdateText()
    {
        if (cardType == CardType.HP)
        {
            cardText.text = "HP + " + value;
        }
        else
        {
            cardText.text = "çUåÇ+" + value;
        }
    }

    public void Onselect()
    {
        if (cardType == CardType.HP)
        {
            if (playerHP != null)
            {
                playerHP.IncreaseMaxHP(value);
            }
        }
    }
}
