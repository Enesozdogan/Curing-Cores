using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITypeWipe : AIType
{
    public AIMeleePlayerDetection playerDetection;
    [SerializeField]
    private bool onCd = false;
    [SerializeField]
    private float cd = 1;
    [SerializeField]
    private Transform wipePos;
    [SerializeField]
    private GameObject wipePrefab;

    private void Start()
    {
        
    }
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
        agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.attack2));
        agent.agentAnimManager.OnAnimAct.AddListener(CreateWipe);
        onCd = true;
        StartCoroutine(AttackCDTimer());
    }
    IEnumerator AttackCDTimer()
    {
        yield return new WaitForSeconds(cd);
        onCd = false;
    }
    private void CreateWipe()
    {
        agent.agentAnimManager.OnAnimAct.RemoveListener(CreateWipe);
        var wipe = Instantiate(wipePrefab, wipePos.position, Quaternion.identity);
        if (agent.transform.localScale.x == -1)
        {
            wipe.transform.localScale = new Vector2(-1, 1);
        }
        Destroy(wipe, 0.3f);
    }
}
  

