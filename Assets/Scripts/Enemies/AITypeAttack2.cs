using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITypeAttack2 : AIType
{
    public AIMeleePlayerDetection playerDetection;
    [SerializeField]
    private bool onCd = false;
    [SerializeField]
    private float cd = 1;

    private void Awake()
    {
        if (playerDetection == null) { playerDetection = GetComponentInChildren<AIMeleePlayerDetection>(); }

    }
   
    public override void Perform(EnemyAIBase enemyBrain)
    {
        if (onCd)
        {
            return;
        }
        if (playerDetection.plFound == false)
        {
            return;
        }
        agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.attack1));
        agent.agentAnimManager.OnAnimActEnd.AddListener(ShiftToAttack2);
        onCd = true;
        StartCoroutine(AttackCDTimer());
    }

    private void ShiftToAttack2()
    {
        agent.agentAnimManager.OnAnimActEnd.RemoveListener(ShiftToAttack2);
        agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.attack2));
        agent.agentAnimManager.OnAnimActEnd.AddListener(ShiftToRoll);

    }

    private void ShiftToRoll()
    {
        agent.agentAnimManager.OnAnimActEnd.RemoveListener(ShiftToRoll);
        agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.roll));
    }

    IEnumerator AttackCDTimer()
    {
        yield return new WaitForSeconds(cd);
        onCd = false;
    }
  
}
