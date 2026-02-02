using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SeamlessBGM : MonoBehaviour
{
    [Header("Setting")]
    public static SeamlessBGM Instance;

    public bool DontDestroyEnabled = true;

    private AudioSource audioSource;

    private void Awake()
    {
        if(Instance!=null &&Instance!=this)
        {
            Destroy(gameObject);
            return;
        }

       
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (scene.name == "StageSelect")
        {
            audioSource.Stop();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
