using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class nowHP : MonoBehaviour
{
    public TextMeshProUGUI hpText;

    private PlayerHP playerhp;//参照するスクリプト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpText = GetComponent<TextMeshProUGUI>();

        playerhp = GameObject.Find("Image_player").GetComponent<PlayerHP>();
    }
    
    void Update()
    {
        hpText.text = " HP  : " + playerhp.Player_Current_Hp;
    }
}
