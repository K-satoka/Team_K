using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] GameObject MenuPanel;

    private void Update()
    {
        if (!MenuPanel.activeSelf) return;

        if(Input.GetKeyDown(KeyCode.Return)||Input.GetKeyDown(KeyCode.Space))
        {
            HidePanel();
        }
    }
    public void ShowPanel()
    {
        MenuPanel.SetActive(true);
    }

    public void HidePanel()
    {
        MenuPanel.SetActive(false);
    }

    public void TogglePanel()
    {
        MenuPanel.SetActive(!MenuPanel.activeSelf);
    }
}
