using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class State_AttackBase : StateBase
{

    public UnityEvent<AudioClip> OnActWeaponSound;
    public LayerMask toBeHitMask;
    protected Vector2 dir;
    int dir_tmp;

    protected override void EnterState()
    {
       
        agent.agentAnimManager.RestartAnimEvents();
        agent.agentAnimManager.Animate(AnimationTypes.attack3);
        
        agent.agentAnimManager.OnAnimAct.AddListener(UseAttack);

        if (agent.transform.localScale.x > 0) dir_tmp = 1;
        else dir_tmp = -1;

        dir = agent.transform.right * dir_tmp;

        if (agent.groundCheck.isGrounded) agent.agentRb2d.velocity = Vector2.zero;
    }
    protected void UseAttack()
    {
        OnActWeaponSound?.Invoke(agent.weaponInfo.soundFx);
        agent.agentAnimManager.OnAnimAct.RemoveListener(UseAttack);
        agent.weaponInfo.UseAttack(agent, toBeHitMask, dir);
    }


}
