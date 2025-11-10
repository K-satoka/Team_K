using UnityEngine;

public class laser : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ìñÇΩÇ¡ÇΩÇüÅI");
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("ínñ Ç…ìñÇΩÇ¡ÇΩÇüÅI");
        }
    }
}
