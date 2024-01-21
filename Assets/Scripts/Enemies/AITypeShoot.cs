using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITypeShoot : AIType
{
    [SerializeField]
    private AIShooterPlayerDetection detectPl;

    [SerializeField]
    private float shootCdVal = 2;
    private bool onCd = false;
    public override void Perform(EnemyAIBase enemyBrain)
    {
        if (onCd == false)
        {
            if (detectPl.isDetected)
            {
                onCd = true;
                enemyBrain.ActivateMovement(detectPl.dir);
                enemyBrain.ActivateAttack();
                StartCoroutine(startCd());
            }
        }
    }
    IEnumerator startCd()
    {
        yield return new WaitForSeconds(shootCdVal);
        onCd = false;
    }

}
