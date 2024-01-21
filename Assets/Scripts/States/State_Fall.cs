using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Fall : State_Move
{
    

    protected override void EnterState()
    {
        agent.agentAnimManager.Animate(AnimationTypes.fall);
    }

    protected override void HandleJumpPressAct()
    {
        
    }
    public override void UpdateInState()
    {
        CalVel();
        SetVel();
        applyGravity();
        if(agent.groundCheck.isGrounded)
        {
            
            data_Move.curVel = Vector2.zero;
            agent.agentRb2d.velocity=data_Move.curVel;
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.idle));
        }
    }
    protected override void HandlePerformSkill()
    {
        if(agent.ringManager.index == (int)RingManager.ringTypes.azure && agent.ringManager.PerformSkill())
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.hook));
        else if (agent.ringManager.index == (int)RingManager.ringTypes.magenta && agent.ringManager.PerformSkill())
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.teleport));
    }
    private void applyGravity()
    {
        data_Move.curVel = agent.agentRb2d.velocity;
        data_Move.curVel.y += agent.agentSO.gravityMultF * Physics2D.gravity.y * Time.deltaTime;
        agent.agentRb2d.velocity = data_Move.curVel;
    }
}
