using UnityEngine;

public class air : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        // ”O‚Ì‚½‚ß‰Šú‰»i255,255,255j
        cam.backgroundColor = Color.white;
    }

    void Update()
    {
        Color c = cam.backgroundColor;

        // g ‚Æ b ‚ğ­‚µ‚¸‚ÂŒ¸‚ç‚·
        c.g -= Time.deltaTime * 0.1f;
        c.b -= Time.deltaTime * 0.1f;

        c.g = Mathf.Clamp01(c.g);
        c.b = Mathf.Clamp01(c.b);

        cam.backgroundColor = c;
    }
}
