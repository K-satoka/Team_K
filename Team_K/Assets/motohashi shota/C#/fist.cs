using UnityEngine;

public class fist : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ìñÇΩÇ¡ÇΩÇüÅI");

            animator.SetTrigger("HIT");
            Destroy(gameObject,0.4f);
        }
        if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("ínñ Ç…ìñÇΩÇ¡ÇΩÇüÅI");
            animator.SetTrigger("HIT");
            Destroy(gameObject,0.4f);
        }
    }
}
