using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理に必要

public class skip_2 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip SkipSE;

    // ボタンが押されたら呼ばれる関数

    private bool isClicked = false; // ボタンが押されたかどうか

    public void ChangeScene()
    {
        if (audioSource != null && SkipSE != null)
            audioSource.PlayOneShot(SkipSE);

        if (isClicked) return; // すでに押されていたら何もしない

        isClicked = true; // 一度押されたことを記録
        FadeManager.Instance.LoadScene("StageSelect", 1.0f);

    }
}