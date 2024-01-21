using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITypeAttack : AIType
{
    public AIMeleePlayerDetection playerDetection;
    [SerializeField]
    private bool onCd=false;
    [SerializeField]
    private float cd = 1;

    private void Awake()
    {
        if(playerDetection== null) { playerDetection = GetComponentInChildren<AIMeleePlayerDetection>();}

    }

    public override void Perform(EnemyAIBase enemyBrain)
    {
        if(onCd)
        {
            return;
        }
        if (playerDetection.plFound == false)
        {
            return;
        }
        enemyBrain.ActivateAttack();
        onCd = true;
        StartCoroutine(AttackCDTimer());
    }
    IEnumerator AttackCDTimer()
    {
        yield return new WaitForSeconds(cd);
        onCd= false;
    }
}
