using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentAnimManager : MonoBehaviour
{
    private Animator animator;
    public UnityEvent OnAnimAct;
    public UnityEvent OnAnimActEnd;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void Animate(AnimationTypes animationTypes)
    {
        switch (animationTypes)
        {
            case AnimationTypes.idle:
                PlayAnim("Idle");
                break;
            case AnimationTypes.run:
                PlayAnim("Run");
                break;
            case AnimationTypes.getHit:
                PlayAnim("GetDamaged");
                break;
            case AnimationTypes.die:
                break;
            case AnimationTypes.jump:
                PlayAnim("Jump");
                break;
            case AnimationTypes.climb:
                break;
            case AnimationTypes.fall:
                PlayAnim("Fall");
                break;
            case AnimationTypes.attack1:
                PlayAnim("Attack1");
                break;
            case AnimationTypes.attack2:
                PlayAnim("Attack2");
                break;
            case AnimationTypes.attack3:
                PlayAnim("Attack3");
                break;
            case AnimationTypes.hook:
                PlayAnim("Grab");
                break;
            case AnimationTypes.bloodSlash:
                PlayAnim("BloodSlash");
                break;
            case AnimationTypes.trail:
                PlayAnim("Trail");
                break;
            case AnimationTypes.death:
                PlayAnim("Death");
                break;
            case AnimationTypes.roll:
                PlayAnim("Roll");
                break;
            case AnimationTypes.slam:
                PlayAnim("Slam");
                break;
            default:
                break;
            
        }
    }
    public void PlayAnim(string name)
    {
        animator.Play(name,-1,0f);
    }
    public void RestartAnimEvents()
    {
        OnAnimAct.RemoveAllListeners();
        OnAnimActEnd.RemoveAllListeners();
    }
    public void InvokeAnimAct()
    {
        OnAnimAct?.Invoke();
    }
   
    public void InvokeAnimActEnd()
    {
        OnAnimActEnd?.Invoke();
    }

}

public enum AnimationTypes
{
    idle,
    run,
    getHit,
    die,
    jump,
    climb,
    fall,
    attack1,
    attack2,
    attack3,
    hook,
    bloodSlash,
    trail,
    death,
    roll,
    slam
}
