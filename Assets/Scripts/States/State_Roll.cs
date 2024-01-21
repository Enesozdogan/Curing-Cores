using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class State_Roll : StateBase
{
    [SerializeField]
    private float rollSpeed;
    public UnityEvent onActRoll;
    private Vector2 dir;
    private int dir_tmp;
    protected override void EnterState()
    {
        agent.agentAnimManager.Animate(AnimationTypes.roll);
        agent.agentAnimManager.OnAnimAct.AddListener(RollActInvoker);
        agent.agentAnimManager.OnAnimActEnd.AddListener(ShiftToIdle);
        if (agent.transform.localScale.x > 0) dir_tmp = 1;
        else dir_tmp = -1;
        dir = agent.transform.right * dir_tmp;
        agent.agentRb2d.velocity=new Vector2(rollSpeed*dir.x,agent.agentRb2d.velocity.y); 
    }

    private void RollActInvoker()
    {
        agent.agentAnimManager.OnAnimAct.RemoveListener(RollActInvoker);
        onActRoll?.Invoke();
    }

    private void ShiftToIdle()
    {
        agent.agentAnimManager.OnAnimActEnd.RemoveListener(ShiftToIdle);
        
        if (agent.groundCheck.isGrounded)
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.idle));
        }
        else if (!agent.groundCheck.isGrounded)
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.fall));
        }
    }
}
