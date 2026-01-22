using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class nowATK : MonoBehaviour
{
    public TextMeshProUGUI atkText;

    private AttackCollision playerAttack;//参照するスクリプト
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        atkText= GetComponent<TextMeshProUGUI>();

        playerAttack = GameObject.Find("Image_player").GetComponent<AttackCollision>();
    }
    void Update()
    {
        atkText.text = "ATK : " + playerAttack.currentAttack;
    }
}
