using UnityEngine;

public class air : MonoBehaviour
{
    Camera cam;
    float r = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Color c = cam.backgroundColor;

        c.r += Time.deltaTime * 0.2f;
        c.b -= Time.deltaTime * 0.2f;

        c.r = Mathf.Clamp01(c.r);
        c.b = Mathf.Clamp01(c.b);

        cam.backgroundColor = c;
    }
}
