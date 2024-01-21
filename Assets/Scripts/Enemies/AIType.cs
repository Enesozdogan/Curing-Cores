using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIType : MonoBehaviour
{
    [SerializeField]
    protected AgentScript agent;
    public abstract void Perform(EnemyAIBase enemyBrain);
    

   
}
