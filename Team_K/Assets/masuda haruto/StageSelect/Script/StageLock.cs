using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class LockTextFade : MonoBehaviour
{
    public TextMeshProUGUI lockText;
    public float fadeDuration = 0.5f; // フェードにかける時間

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);

        // 初期は透明
        SetTextAlpha(0f);
    }

    void OnButtonClicked()
    {
        StartCoroutine(FadeInText());
    }

    IEnumerator FadeInText()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            SetTextAlpha(alpha);
            yield return null;
        }
        SetTextAlpha(1f); // 完全表示
    }

    void SetTextAlpha(float alpha)
    {
        Color c = lockText.color;
        c.a = alpha;
        lockText.color = c;
    }
}
