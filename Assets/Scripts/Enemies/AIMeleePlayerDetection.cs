using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIMeleePlayerDetection : MonoBehaviour
{
    public bool plFound { get; internal set; }
    [HideInInspector]
    public bool canRoll1;
    [HideInInspector]
    private State_AttackBase attackBase;

    public LayerMask plMask;
    public UnityEvent<GameObject> OnActPlDetection;

    public float range;
    public Color gizmoCol = Color.green;
    public bool gizmoOn;

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, range, plMask);
        if (col != null)
        {
            plFound= true;
            OnActPlDetection?.Invoke(col.gameObject);
            
        }
        else
        {
            plFound = false;
            canRoll1 = false;
        }
    }
    private void OnDrawGizmos()
    {
        if (gizmoOn)
        {
            Gizmos.color = gizmoCol;
            Gizmos.DrawSphere(transform.position, range);
        }
    }
    

}
