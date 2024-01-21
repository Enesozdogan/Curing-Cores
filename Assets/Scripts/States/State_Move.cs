using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class State_Move : StateBase
{
    [SerializeField]
    protected Data_Move data_Move;

    public UnityEvent OnStepAct;
   

    
    private void Awake()
    {
        data_Move=GetComponentInParent<Data_Move>();
    }
    protected override void EnterState()
    {
        agent.agentAnimManager.OnAnimAct.AddListener(()=>OnStepAct.Invoke());
        agent.agentAnimManager.Animate(AnimationTypes.run);
        data_Move.horMovDir = 0;
        data_Move.curSpeed = 0;
        data_Move.curVel = Vector2.zero;

    }
    
    protected override void HandleMovAct(Vector2 obj)
    {
        base.HandleMovAct(obj);
    }
    public override void UpdateInState()
    {
        if (CheckFalling()) return;
        CalVel();
        SetVel();
        if (Mathf.Abs(agent.agentRb2d.velocity.x) < 0.01f)
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.idle));

        }
    }

    protected void SetVel()
    {
        agent.agentRb2d.velocity = data_Move.curVel;
    }

    protected void CalVel()
    {
        CalSpeed(agent.inputController.MovVec, data_Move);
        CalHorDir(data_Move);
        data_Move.curVel = data_Move.horMovDir * data_Move.curSpeed * Vector3.right ;
        data_Move.curVel.y = agent.agentRb2d.velocity.y;
    }

    protected void CalSpeed(Vector2 movVec, Data_Move data_Move)
    {
        if (Mathf.Abs(movVec.x) > 0)
        {
            data_Move.curSpeed += agent.agentSO.acc * Time.deltaTime;
        }
        else
        {
            data_Move.curSpeed -= agent.agentSO.deacc* Time.deltaTime;
        }
        data_Move.curSpeed = Mathf.Clamp(data_Move.curSpeed, 0, agent.agentSO.speedMax);
    }

    protected void CalHorDir(Data_Move data_Move)
    {
        if (agent.inputController.MovVec.x > 0)
        {
            data_Move.horMovDir = 1;
        }
        else if(agent.inputController.MovVec.x < 0)
        {
            data_Move.horMovDir = -1;
        }
    }
    protected override void ExitState()
    {
        agent.agentAnimManager.RestartAnimEvents();
    }
    protected override void HandlePerformSkill()
    {
          if (agent.ringManager.index == (int)RingManager.ringTypes.magenta && agent.ringManager.PerformSkill())
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.teleport));
    }

}
