using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color originalColor;//元の色
    public float flash = 0.1f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr=GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    //赤くするコールチン
    IEnumerator FlashRoutine()
    {
        sr.color = Color.red;

        yield return new WaitForSeconds(flash);

        sr.color = originalColor; ;
    }
}
