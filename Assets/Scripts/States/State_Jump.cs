using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Jump : State_Move
{

   
    
    public bool jumPres = false;

  
    protected override void EnterState()
    {
        
        agent.agentAnimManager.Animate(AnimationTypes.jump);
        data_Move.curVel=agent.agentRb2d.velocity;
        data_Move.curVel.y = agent.agentSO.upForce;
       
        agent.agentRb2d.velocity = data_Move.curVel;
        jumPres= true;

    }
    protected override void HandleJumpPressAct()
    {
        jumPres= true;
    }
    protected override void HandleJumpReleasedAct()
    {
        jumPres= false;
    }
    public override void UpdateInState()
    {
        CalVel();
        SetVel();
        applyGravity();
        
        if (agent.agentRb2d.velocity.y <= 0)
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.fall));
        }
    }

    protected override void HandlePerformSkill()
    {
        if (agent.ringManager.index == (int)RingManager.ringTypes.azure && agent.ringManager.PerformSkill())
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.hook));
        else if(agent.ringManager.index == (int)RingManager.ringTypes.magenta && agent.ringManager.PerformSkill())
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.teleport));
    }

    private void applyGravity()
    {
        if(jumPres==false)
        {
            data_Move.curVel = agent.agentRb2d.velocity;
            data_Move.curVel.y += agent.agentSO.gravityMultJ* Physics2D.gravity.y * Time.deltaTime;
            agent.agentRb2d.velocity = data_Move.curVel;
        }
    }
}
