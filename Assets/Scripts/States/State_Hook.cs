using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Hook : State_Move
{
   
    [SerializeField]
    private DistanceJoint2D joint;
    [SerializeField]
    private Transform hookPoint;
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private AzureRing azure;


   
    protected override void EnterState()
    {
        joint.enabled = true;
        lineRenderer.enabled = true;
        joint.connectedAnchor = azure.hingeObject.transform.position;
        SetLineRenderer();
        agent.agentAnimManager.Animate(AnimationTypes.hook);
        
    }


    private void ShiftToIdle()
    {
  
            if (agent.groundCheck.isGrounded)
            {
                agent.agentRb2d.velocity = Vector2.zero;
                BreakHook();
                agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.idle));
            }
            else if(!azure.canHook)
            {
                BreakHook();
                agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.fall));
            }
        
    }
    public override void UpdateInState()
    {
       
        applyGravity();
        ShiftToIdle();
        SetLineRenderer();
        
        
    }
   
    private void SetLineRenderer()
    {
        if (lineRenderer.enabled == false) return;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, hookPoint.transform.position);
        lineRenderer.SetPosition(1, azure.hingeObject.transform.position);
    }
    protected override void HandlePerformSkill()
    {

        BreakHook();
        ShiftToIdle();
    }
    private void BreakHook()
    {
        joint.enabled = false;
        lineRenderer.enabled = false;
        azure.hingeObject = null;
        azure.canHook=false;
    }
    private void applyGravity()
    {
        data_Move.curVel = agent.agentRb2d.velocity;
        data_Move.curVel.y += agent.agentSO.gravityMultF * Physics2D.gravity.y * Time.deltaTime;
        agent.agentRb2d.velocity = data_Move.curVel;
    }
    protected override void HandleAttackAct()
    {
        
    }
}
