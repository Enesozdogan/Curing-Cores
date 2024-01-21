using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class State_BloodSlash : StateBase
{
    [SerializeField]
    private GameObject napalmPrefab;
    [SerializeField] 
    private GameObject napalmGO;
    [SerializeField]
    private float offSetX,offSetY;

    [SerializeField]
    private float range;
    [SerializeField]
    private bool gizmoOn = false;

    [SerializeField]
    private LayerMask toBeHitMask;
    [SerializeField]
    private Color gizmoColor;
    private Vector2 dir;
    [SerializeField]
    private Vector2 boxCastSize;
    [SerializeField]
    private float napalmDuration;
    private Vector2 offSet;
    private int dir_tmp;
    [SerializeField]
    private bool isDone;
   
    private int count = 0;
    protected override void EnterState()
    {
        gizmoOn = true;
        if (agent.transform.localScale.x > 0) dir_tmp = 1;
        else dir_tmp = -1;

        dir = agent.transform.right * dir_tmp;
        agent.agentAnimManager.Animate(AnimationTypes.bloodSlash);
        agent.agentAnimManager.OnAnimActEnd.AddListener(ShiftToIdle);
        agent.agentAnimManager.OnAnimAct.AddListener(CreateNapalm);
    }
    private void ShiftToIdle()
    {
        agent.agentAnimManager.OnAnimActEnd.RemoveListener(ShiftToIdle);
       
        
        if (agent.groundCheck.isGrounded)
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.idle));
        }
        else if (!agent.groundCheck.isGrounded)
        {
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.fall));
        }
    }

    private void CreateNapalm()
    {
        
        count++;
        if (count == 2)
        {
            count = 0;
            return;
        }
       
        offSet.x = offSetX;
        offSet.y = offSetY;
        if (dir_tmp == -1)
        {
            offSet.x *= -1;
        }
        agent.agentAnimManager.OnAnimAct.RemoveListener(CreateNapalm);
        napalmGO=Instantiate(napalmPrefab, (Vector2)transform.position+offSet, Quaternion.identity);
        DamageEnemies();
        StartCoroutine(Timer());
        
       

    }



    private void DamageEnemies()
    {
     

        List<IToBeHit> enemiesHit = new List<IToBeHit>();
        RaycastHit2D[] hits = Physics2D.RaycastAll(agent.transform.position, dir, range, toBeHitMask);

        foreach (RaycastHit2D hit in hits)
        {
            IToBeHit enemy = hit.collider.GetComponent<IToBeHit>();
            if (enemy != null && !enemiesHit.Contains(enemy))
            {
                enemiesHit.Add(enemy);
                enemy.GetDamaged(agent.gameObject, 40);
            }
        }

    }



    IEnumerator Timer()
    {
        yield return new WaitForSeconds(napalmDuration);
        
        Destroy(napalmGO);
    }
    private void OnDrawGizmos()
    {
        if (gizmoOn && napalmGO!=null)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawLine(agent.transform.position, agent.transform.position+(Vector3)dir*range);
        }
    }
    protected override void HandleAttackAct()
    {
       
    }
    protected override void ExitState()
    {
        gizmoOn = false;
    }
    protected override void HandleJumpPressAct()
    {

    }
    protected override void HandleJumpReleasedAct()
    {

    }
    protected override void HandleMovAct(Vector2 obj)
    {

    }
    protected override void HandleSwapRingAct()
    {

    }
    public override void UpdateInState()
    {

    }
    protected override void HandlePerformSkill()
    {
        
    }
}
