using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickableBase : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    private Color gizColor=Color.red;
    [SerializeField]
    protected BoxCollider2D pickableCol;
    protected abstract void PickUpAction(AgentScript agent);

    private void Awake()
    {
        spriteRenderer= GetComponentInChildren<SpriteRenderer>();
        pickableCol=GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickUpAction(collision.GetComponent<AgentScript>());
            Destroy(this.gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = gizColor;
        Gizmos.DrawCube(pickableCol.bounds.center,pickableCol.bounds.size);
    }



}
