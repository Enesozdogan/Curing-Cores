using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class State_Teleport : State_Move
{
    [SerializeField]
    private MagentaRing magentaRing; 
    [SerializeField]
    private float delayCounter,trailDelay;
    [SerializeField]
    private float rangeMul=1;
    [SerializeField]
    private LayerMask obstacleLayerMask;
    [SerializeField]
    private GameObject trailObj;
    [SerializeField]
    private SpriteRenderer agentSprite;
    [SerializeField]
    private GameObject trailParent;
    private Vector2 dir;
    private int dir_tmp;
    private float gravity_tmp;
    protected override void EnterState()
    {
        agent.agentAnimManager.Animate(AnimationTypes.trail);
        delayCounter = trailDelay;

        if (agent.transform.localScale.x > 0) dir_tmp = 1;
        else dir_tmp = -1;
        dir = agent.transform.right * dir_tmp;
        data_Move.curVel = agent.agentRb2d.velocity;
        if(dir_tmp>0) data_Move.curVel = new Vector2(agent.agentSO.teleportDist,0);
        else data_Move.curVel = new Vector2(agent.agentSO.teleportDist*-1,0);

        agent.agentRb2d.velocity = data_Move.curVel;
        
        gravity_tmp=agent.agentRb2d.gravityScale;
        agent.agentRb2d.gravityScale = 0;

    }
    public override void UpdateInState()
    {
        if (magentaRing.makeTrail)
        {
            if (delayCounter > 0)
                delayCounter -= Time.deltaTime;
            else
            {

                GameObject trailGO = Instantiate(trailObj, transform.parent.position, Quaternion.identity);
                trailGO.GetComponent<SpriteRenderer>().sprite=agentSprite.sprite;
                trailGO.transform.localScale = agent.transform.localScale;
                trailGO.transform.SetParent(trailParent.transform);
                Destroy(trailGO, 0.5f);
                delayCounter = trailDelay;
            }
        }
        else
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.idle));
        }
    }

    private bool NoObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(agent.transform.position, dir, rangeMul ,obstacleLayerMask);
        if (hit.collider != null)
        {
            return false;
        }
        return true;
    }
    
    protected override void ExitState()
    {
        data_Move.curVel = agent.agentRb2d.velocity;
        data_Move.curVel = new Vector2(0, agent.agentRb2d.velocity.y);
        agent.agentRb2d.velocity = data_Move.curVel;
        agent.agentRb2d.gravityScale = gravity_tmp;
    }

    protected override void HandleMovAct(Vector2 obj)
    {
        
    }
    protected override void HandleAttackAct()
    {
        
    }
    protected override void HandleJumpPressAct()
    {
        
    }
    protected override void HandlePerformSkill()
    {
        
    }
    protected override void HandleSwapRingAct()
    {
        
    }
    protected override void HandleJumpReleasedAct()
    {
        
    }
}
