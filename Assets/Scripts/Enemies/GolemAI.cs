using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAI : EnemyAIBase
{
    [SerializeField]
    private AIType slamType, attackType,wipeType;
    private bool onCd = false;

    private void Update()
    {
        if (onCd == false)
        {
            onCd = true;
            PerformMelee();
            StartCoroutine(Cooldown());
        }
            
        
        slamType.Perform(this);
    }

    private void PerformMelee()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        if(rand==0)
            attackType.Perform(this);
        else
            wipeType.Perform(this);
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        onCd = false;
    }
}
