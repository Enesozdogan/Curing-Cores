using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public  abstract class StateBase : MonoBehaviour
{
    protected AgentScript agent;
    public UnityEvent OnEnterState, OnExitState;

    

    
    public void StateInitializer(AgentScript agent)
    {
        this.agent = agent;
    }
    public void Enter()
    {
        
        agent.inputController.OnActAttack += HandleAttackAct;
        agent.inputController.OnActJumpPressed += HandleJumpPressAct;
        agent.inputController.OnActJumpReleased += HandleJumpReleasedAct;
        agent.inputController.OnActMovement += HandleMovAct;
        agent.inputController.OnActRingSwap += HandleSwapRingAct;
        agent.inputController.OnActPerformSkill += HandlePerformSkill;
        OnEnterState?.Invoke();
        EnterState();
    }
    protected virtual void HandlePerformSkill()
    {
        if (agent.ringManager.index == (int)RingManager.ringTypes.crimson && agent.ringManager.PerformSkill())
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.bloodSlash));
        else if (agent.ringManager.index == (int)RingManager.ringTypes.magenta && agent.ringManager.PerformSkill())
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.teleport));
    }
    protected virtual void EnterState()
    {
        
    }

    protected virtual void HandleSwapRingAct()
    {
        
    }

    protected virtual void HandleMovAct(Vector2 obj)
    {
       
    }

    protected virtual void HandleJumpReleasedAct()
    {
        
    }
   
    protected virtual void HandleJumpPressAct()
    {
        CanJump();
    }

    private void CanJump()
    {
      
        if(agent.groundCheck.isGrounded)
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.jump));
        }
    }

    protected virtual void HandleAttackAct()
    {
      
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.attack1));
        
       
    }
    public virtual void UpdateInState()
    {
        CheckFalling();
    }
  
    protected bool CheckFalling()
    {
        if(agent.groundCheck.isGrounded==false)
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.fall));
            return true;
        }
        return false;
    }

    public virtual void FixedUpdateState()
    {

    }
    public virtual void HandleDeathAct()
    {
        agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.death));
    }

    public void Exit()
    {
        
        agent.inputController.OnActAttack -= HandleAttackAct;
        agent.inputController.OnActJumpPressed -= HandleJumpPressAct;
        agent.inputController.OnActJumpReleased -= HandleJumpReleasedAct;
        agent.inputController.OnActMovement -= HandleMovAct;
        agent.inputController.OnActRingSwap -= HandleSwapRingAct;
        agent.inputController.OnActPerformSkill -= HandlePerformSkill;
        OnExitState?.Invoke();
        ExitState();
    }

    protected virtual void ExitState()
    {
        
    }

    public virtual void GetDamaged()
    {

        agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.getDamaged));
    }
}
