using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopFromFalling : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D agentRb2d;

    private void Awake()
    {
        agentRb2d=GetComponent<Rigidbody2D>();
    }
    public void SetVelZero()
    {
        agentRb2d.velocity= Vector2.zero;
    }
}
