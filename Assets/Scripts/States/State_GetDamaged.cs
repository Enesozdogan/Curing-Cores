using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_GetDamaged : StateBase
{
    protected override void EnterState()
    {
        agent.agentRb2d.velocity = new Vector2(0, agent.agentRb2d.velocity.y);
        agent.agentAnimManager.Animate(AnimationTypes.getHit);
        agent.agentAnimManager.OnAnimActEnd.AddListener(ShiftToIdle);
    }

    private void ShiftToIdle()
    {
        agent.agentAnimManager.OnAnimActEnd.RemoveListener(ShiftToIdle);
        agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.idle));
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
    public override void UpdateInState()
    {
        
    }
    public override void FixedUpdateState()
    {
        
    }
}
