using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class NewMonoBehaviourScript : MonoBehaviour
{

    Animation animator;

    public string attackAnime = "Player_attack";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animation>();
    }

    private void Update()
    {
       
    }
    // Update is called once per frame
    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if(context.interaction is TapInteraction)
        {
            animator.Play(attackAnime);
        }
    }
}
