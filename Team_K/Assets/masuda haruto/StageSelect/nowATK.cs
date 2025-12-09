using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class nowATK : MonoBehaviour
{
    public TMP_Text atkText;
    public TMP_Text hpText;

    private Player_Attack playerAttack;
    private PlayerHP playerHP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAttack= GetComponent<Player_Attack>();
       playerHP= GetComponent<PlayerHP>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //     atkText.text = "ATK:" + playerAttack.attackDamage;
    //}
}
