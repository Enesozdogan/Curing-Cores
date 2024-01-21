using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data_Agent",menuName ="Agent/CreateData")]
public class AgentSO : ScriptableObject
{

    [Header("Movement Data")]
    [Space]
    public int health = 2;

    [Header("Movement Data")]
    [Space]
    public float acc ;
    public float deacc;
    public float speedMax;

    [Header("Jump Data")]
    [Space]
    public float upForce = 10;
    public float gravityMultJ = 1.5f;
    public float gravityMultF = 0.5f;

    [Header("Teleport Data")]
    [Space]
    public float teleportDist = 100;
}
