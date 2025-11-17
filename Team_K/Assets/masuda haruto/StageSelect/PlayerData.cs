using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public int maxHP_Up = 0;
    public int attack_up = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetData()
    {
        attack_up = 0;
        maxHP_Up = 0;
    }
    


}
