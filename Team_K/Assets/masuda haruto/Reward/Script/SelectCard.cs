using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCard : MonoBehaviour,
    ISelectHandler,IDeselectHandler
{
    public float selectedScale = 1.1f;
    public float normalScale = 1.0f;
    public float speed = 10f;

    private Vector3 targetScale;

    void Awake()
    {
        targetScale = Vector3.one * normalScale;
        transform.localScale = targetScale;
    }

    void Update()
    {
        transform.localScale = 
            Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }

    public void OnSelect(BaseEventData eventData)
    {
        targetScale = Vector3.one * selectedScale;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        targetScale=Vector3.one * normalScale;
    }

}

