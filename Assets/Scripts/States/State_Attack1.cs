using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class State_Attack1 : State_AttackBase
{
   
  
    [SerializeField]
    private bool gizmoAct = true;
    [SerializeField]
    private bool bool_attack2;
    protected override void EnterState()
    {
        base.EnterState();
        agent.agentAnimManager.Animate(AnimationTypes.attack1);
        agent.agentAnimManager.OnAnimActEnd.AddListener(ShiftToIdle);
        
        bool_attack2 = false;
       
    }

  

    private void ShiftToIdle()
    {
        agent.agentAnimManager.OnAnimActEnd.RemoveListener(ShiftToIdle);
        if (bool_attack2)
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.attack2));
        }
        else if (agent.groundCheck.isGrounded )
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.idle));
        }
        else if(!agent.groundCheck.isGrounded )
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.fall));
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (gizmoAct)
        {
            Gizmos.color = Color.yellow;
            agent.weaponInfo.WeaponGiz(agent.weaponTransform.transform.position,dir);
        }
    }
 
    protected override void HandleAttackAct()
    {
        bool_attack2 = true;
    }
    protected override void ExitState()
    {
        
        agent.agentAnimManager.RestartAnimEvents();
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
    public override void UpdateInState()
    {
       
    }
    public override void GetDamaged()
    {
        
    }
}
