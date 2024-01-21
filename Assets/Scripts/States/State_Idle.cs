using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class State_Idle : StateBase
{
   
    protected override void EnterState()
    {
        agent.agentAnimManager.Animate(AnimationTypes.idle);
        agent.agentRb2d.velocity = new Vector2(0, agent.agentRb2d.velocity.y);
    }
    protected override void HandleMovAct(Vector2 inputDir)
    {
        if (Mathf.Abs(inputDir.x) > 0)
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.move));
        }
    }
    public override void UpdateInState()
    {
       if(CheckFalling()) { return; }
        if (Mathf.Abs(agent.inputController.MovVec.x) > 0)
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.move));
        }
    }
    protected override void HandlePerformSkill()
    {
          base.HandlePerformSkill();
          if (agent.ringManager.index == (int)RingManager.ringTypes.magenta && agent.ringManager.PerformSkill())
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.teleport));
    }
}
