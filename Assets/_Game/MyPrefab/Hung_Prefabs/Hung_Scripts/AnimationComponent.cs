using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayMove()
    {
        animator.SetBool("Run", true);
    }
    public void PlayIdle()
    {
        animator.SetBool("Run", false);
    }

    public void PlayTrigger(string animationTrigger) 
    {
        animator.SetTrigger(animationTrigger);
    }
}
