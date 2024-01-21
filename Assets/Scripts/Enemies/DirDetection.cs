using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DirDetection : MonoBehaviour
{
    public event Action OnActChangeDir;
    public BoxCollider2D detectionCol;
    public LayerMask playerMask;
    public LayerMask grMask;
    public float grRayCastRange = 2;
    private float wallRayCastRange = .7f;
    [Range(0, 1)]
    public float grRayCastCd = .1f;
    public bool changeDir { get; private set; }
    public bool canChange = true;

    
    [Header("Gizmo Values")]
    public Color colColor = Color.yellow;
    public Color grColor = Color.green;
    public Color playerColor = Color.blue;
    public bool hitTheWall = false;
    public bool gizmoOn = true;

   
    private void Start()
    {
        StartCoroutine(GroundCheckRoutine());
        StartCoroutine(PlayerDetectionRoutine());

    }
    


    IEnumerator GroundCheckRoutine()
    {
        yield return new WaitForSeconds(grRayCastCd);
        var rayHit = Physics2D.Raycast(detectionCol.bounds.center, Vector2.down, grRayCastRange, grMask);
        var rayHit2 = Physics2D.Raycast(detectionCol.bounds.center, Vector2.right*transform.parent.localScale.x, wallRayCastRange, grMask);
        if (rayHit.collider == null)
        {
            OnActChangeDir?.Invoke();
            hitTheWall = false;
            changeDir = true;
        }
        else
        {
           if(rayHit2.collider != null)
            {
                OnActChangeDir?.Invoke();
            }
        }
        StartCoroutine(GroundCheckRoutine());
    }
    
    IEnumerator PlayerDetectionRoutine()
    {
        yield return new WaitForSeconds(0.01f);
        Collider2D col = Physics2D.OverlapCircle((Vector2)transform.position, 4, playerMask);
        if(col!=null )
        {
            if(col.transform.localScale.x == transform.parent.localScale.x)
            {
                if (col.transform.localScale.x == 1)
                {
                    if(col.transform.position.x < transform.parent.position.x)
                        OnActChangeDir?.Invoke();
                }
                else
                {
                    if (col.transform.position.x > transform.parent.position.x)
                        OnActChangeDir?.Invoke();
                }
            }
            
            //Debug.Log("Object Name: " + col.name);
           
        }
            
        StartCoroutine(PlayerDetectionRoutine());
            
    }
 
    private void OnDrawGizmos()
    {
        if (gizmoOn)
        {
            Gizmos.color = playerColor;
            Gizmos.DrawWireSphere((Vector2)transform.position , 4);

            Gizmos.color = grColor;
            Gizmos.DrawRay(detectionCol.bounds.center, Vector2.down * grRayCastRange);
            Gizmos.color = grColor;
            Gizmos.DrawRay(detectionCol.bounds.center, Vector2.right * transform.parent.localScale.x * wallRayCastRange);


            Gizmos.color = colColor;
            Gizmos.DrawCube(detectionCol.bounds.center, detectionCol.bounds.size);
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        canChange = true;
    }
}
