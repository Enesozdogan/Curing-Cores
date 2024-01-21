using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackEffect : MonoBehaviour,IToBeHit
{

    [SerializeField]
    private Rigidbody2D agentRb2d;
    [SerializeField]
    private float forceMag=5;
    [SerializeField]
    private float velX;
    private void Awake()
    {
        agentRb2d= GetComponent<Rigidbody2D>(); 
    }


    public void GetDamaged(GameObject enemy, int dmg)
    {
        
        Vector2 dir = transform.position - enemy.transform.position;
        agentRb2d.AddForce(new Vector2(dir.normalized.x,0)* forceMag, ForceMode2D.Impulse);
        velX = agentRb2d.velocity.x;
    }

}
