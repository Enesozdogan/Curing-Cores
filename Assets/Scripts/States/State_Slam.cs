using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class State_Slam : StateBase
{
    [SerializeField]
    private GameObject slamPrefab;
    [SerializeField]
    private float boxWidth,boxHeight;
    [SerializeField]
    private float offset;
    [SerializeField]
    private LayerMask toBeHitMask;
    public UnityEvent OnActSlamSound;

    protected override void EnterState()
    {
        agent.agentAnimManager.Animate(AnimationTypes.slam);
        agent.agentAnimManager.OnAnimAct.AddListener(CreateSlam);
        agent.agentAnimManager.OnAnimActEnd.AddListener(ShiftToIdle);
        
    }

    private void CreateSlam()
    {
        
        List<IToBeHit> enemiesHit = new List<IToBeHit>();
        Collider2D[] hits = Physics2D.OverlapBoxAll(agent.transform.position, new Vector2(boxWidth, boxHeight), 0, toBeHitMask);

        foreach (Collider2D hit in hits)
        {
            IToBeHit enemy = hit.GetComponent<IToBeHit>();
            if (enemy != null && !enemiesHit.Contains(enemy))
            {
                enemiesHit.Add(enemy);
                
                var slam = Instantiate(slamPrefab, new Vector2(hit.gameObject.transform.position.x, agent.transform.position.y+offset), Quaternion.identity);
                OnActSlamSound?.Invoke();
                enemy.GetDamaged(agent.gameObject, 5);
                Destroy(slam, 1);
               
            }
        }
    }

    private void ShiftToIdle()
    {
        agent.agentAnimManager.OnAnimActEnd.RemoveListener(ShiftToIdle);
      
        if (agent.groundCheck.isGrounded)
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.idle));
        }
        else
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.fall));
        }
    }
    
    public override void GetDamaged()
    {
        
    }
}
