using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private const string FALLING = "isFalling";
    private const string RUNNING = "isRunning";
    private const string CRUSH = "isCollide";
    private const string VICTORY = "isVictory";

    // Animate Idle
    public void GoIdle()
    {
        animator.SetBool(FALLING, false);
        animator.SetBool(RUNNING, false);
        animator.SetBool(CRUSH, false);
    }

    // Animate Falling
    public void GoFall()
    {
        animator.SetBool(FALLING, true);
        animator.SetBool(RUNNING, false);
        animator.SetBool(CRUSH, false);
    }

    // Animate Running
    public void GoRun()
    {
        animator.SetBool(FALLING, false);
        animator.SetBool(RUNNING, true);
        animator.SetBool(CRUSH, false);
    }

    // Animate Crush
    public void GoCollide()
    {
        animator.SetBool(FALLING, false);
        animator.SetBool(RUNNING, false);
        animator.SetBool(CRUSH, true);
    }

    // Animate Victory
    public void GoVictory()
    {
        animator.SetBool(VICTORY, true);
    }

}
