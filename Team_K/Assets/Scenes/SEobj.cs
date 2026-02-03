using UnityEngine;
using UnityEngine.Audio;

public class SEobj : MonoBehaviour
{
    private static SEobj instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ÉVÅ[ÉìÇÇ‹ÇΩÇ¢Ç≈Ç‡écÇÈ
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void PlaySE(AudioClip clip, float volume = 1.0f)
    {
        if (clip == null) return;

        GameObject seObj = new GameObject("OneShotSE");
        AudioSource audio = seObj.AddComponent<AudioSource>();

        audio.clip = clip;
        audio.volume = volume;
        audio.Play();

        Destroy(seObj, clip.length);
    }
}
