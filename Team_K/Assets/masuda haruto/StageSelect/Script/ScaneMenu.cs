using UnityEngine;
using UnityEngine.SceneManagement;

public class ScaneMenu : MonoBehaviour
{
    public void LoadSceneByName(string StatusMenu)
    {
        SceneManager.LoadScene("StatusMenu");
    }

    public void LoadSceneByIndex(int StatusMenu)
    {
        SceneManager.LoadScene(StatusMenu);
    }
}
