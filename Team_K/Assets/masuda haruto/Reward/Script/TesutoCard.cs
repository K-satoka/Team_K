using UnityEngine;
using UnityEngine.UI;

public class CardClickTest : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(OnClick);
        else
            Debug.Log("Buttonが見つかりません！");
    }

    public void OnClick()
    {
        Debug.Log("クリック反応OK！");
    }
}
