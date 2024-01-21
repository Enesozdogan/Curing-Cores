using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Death : StateBase
{
   
    [SerializeField]
    private float fadeAmount, fadeDelay,alphaVal;
    [SerializeField] 
    private SpriteRenderer spriteRenderer;
    protected override void EnterState()
    {
        agent.agentAnimManager.Animate(AnimationTypes.death);
        agent.agentAnimManager.OnAnimActEnd.AddListener(WaitForAct);
        agent.agentAnimManager.OnAnimAct.AddListener(CallFadeOut);
    }
    private void CallFadeOut()
    {
        agent.agentAnimManager.OnAnimAct.RemoveListener(CallFadeOut);
        StartCoroutine(FadeObject());
    }
    private void WaitForAct()
    {
        agent.agentAnimManager.OnAnimActEnd.RemoveListener(WaitForAct);
        agent.OnActDeath?.Invoke();
        
    }
  
    protected override void ExitState()
    {
        StopAllCoroutines();
        agent.agentAnimManager.RestartAnimEvents();
    }

    public IEnumerator FadeObject()
    {
       

        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;

            while (color.a > 0f)
            {
                color.a -= fadeAmount;
                spriteRenderer.color = color;
                alphaVal = color.a;

                
                if (fadeDelay > 0f)
                {
                    yield return new WaitForSeconds(fadeDelay);
                }
            }
        }
    }

    protected override void HandleAttackAct()
    {

    }
    protected override void HandleJumpPressAct()
    {

    }
    protected override void HandleJumpReleasedAct()
    {

    }
    protected override void HandleMovAct(Vector2 obj)
    {

    }
    protected override void HandleSwapRingAct()
    {

    }
    protected override void HandlePerformSkill()
    {

    }
    public override void HandleDeathAct()
    {
        
    }
    public override void GetDamaged()
    {
        
    }
    public override void UpdateInState()
    {
        agent.agentRb2d.velocity=new Vector2(0,agent.agentRb2d.velocity.y);
    }
    public override void FixedUpdateState()
    {

    }
}
