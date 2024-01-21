using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITypeBeam : AIType
{
    public AIMeleePlayerDetection playerDetection;
    [SerializeField]
    private bool onCd = false;
    [SerializeField]
    private float cd = 1;
    [SerializeField]
    private Transform beamPos;
    [SerializeField]
    private GameObject beamPrefab;

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
        enemyBrain.ActivateAttack();
        CreateBeam();
        onCd = true;
        StartCoroutine(AttackCDTimer());
    }
    IEnumerator AttackCDTimer()
    {
        yield return new WaitForSeconds(cd);
        onCd = false;
    }
    private void CreateBeam()
    {
       
        var beam = Instantiate(beamPrefab,beamPos.position, Quaternion.identity) ;
        if (agent.transform.localScale.x == -1)
        {
            beam.transform.localScale = new Vector2(-1,1);
        }
        Destroy(beam, 0.3f);
    }
}
