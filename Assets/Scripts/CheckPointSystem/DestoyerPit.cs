using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyerPit : MonoBehaviour
{
    [SerializeField]
    private Vector2 pitSize;
    [SerializeField]
    private LayerMask targetObjectLM;
    [Header("Colors")]
    [SerializeField]
    private Color pitColor = Color.blue;
    [SerializeField]
    private bool actColor = true;


    private void FixedUpdate()
    {
        Collider2D col=Physics2D.OverlapBox(transform.position, pitSize,0,targetObjectLM);
        if (col != null)
        {
            AgentScript agentInstance=col.GetComponent<AgentScript>();
            if (agentInstance == null)
            {
                Destroy(col.gameObject);
                return;
            }
            var dmg=agentInstance.GetComponent<DmgAndHeal>();
            if(dmg!= null)
            {
                dmg.GetDamaged(20);
                if(dmg.CompareTag("Player") && dmg.CurrHp == 0)
                {
                    agentInstance.GetComponent<FindCPManager>().Spawn();
                }
            }
            agentInstance.AgentDead();

          
        }
    }
    private void OnDrawGizmos()
    {
        if(actColor)
        {
            Gizmos.color = pitColor;
            Gizmos.DrawCube(transform.position, pitSize);
        }
    }
}
