using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITypeSlam : AIType
{
    [SerializeField]
    private AIShooterPlayerDetection detectPl;
    [SerializeField]
    private Transform golemPos;

    [SerializeField]
    private float slamCdVal = 2,limit;
    private bool onCd = false;
    
    public override void Perform(EnemyAIBase enemyBrain)
    {
        if (onCd == false)
        {
            if (detectPl.isDetected && Mathf.Abs(golemPos.transform.position.x - detectPl.Player.transform.position.x) > limit)
            {
                
                onCd = true;
                enemyBrain.ActivateMovement(detectPl.dir);
                agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.slam));
                StartCoroutine(startCd());
            }
        }
    }
    IEnumerator startCd()
    {
        yield return new WaitForSeconds(slamCdVal);
        onCd = false;
    }

}
