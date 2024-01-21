using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzureRing : RingBase
{

    [SerializeField]
    private LayerMask hookLayer;
    [SerializeField]
    private bool gizmoOn=false;
    [SerializeField]
    private Color gizmoColor;
    [SerializeField]
    public GameObject hingeObject;

    public bool canHook = false;
    public override void OnEnter()
    {
        gizmoOn = true;
        agent.agentSO.upForce +=.5f;
        agent.agentSO.gravityMultJ += 1f;
        agent.agentSO.speedMax += .5f;
    }

    public override bool PerformSkill()
    {
        if (DetectHinge())
        {
            canHook = true;
            return true;
        }
        canHook= false;
        return false;

    }

    public override void UpdateMatColor()
    {
        auraMat.SetColor("_Color", outlineColor);
    }

    private bool DetectHinge()
    {
        Collider2D col = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, 2), 3, hookLayer);
        if (col != null)
        {
            hingeObject = col.gameObject;
            return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        if (gizmoOn)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(0, 2), 3);
        }
      
    }
    public override void OnExit()
    {
        agent.agentSO.upForce -= .5f;
        agent.agentSO.gravityMultJ -= 1f;
        agent.agentSO.speedMax -= .5f;
        canHook = false;
        gizmoOn=false;
    }


}
