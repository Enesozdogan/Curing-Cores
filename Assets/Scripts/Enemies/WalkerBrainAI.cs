using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerBrainAI : EnemyAIBase
{
    public GroundCheck groundCheck;
    public AIType attackAI, patrollerAI;

    private void Awake()
    {
        if(groundCheck==null) groundCheck = GetComponentInChildren<GroundCheck>();
    }
    private void Update()
    {
        if (groundCheck.isGrounded)
        {
            attackAI.Perform(this);
            patrollerAI.Perform(this);    
        }
    }
}
