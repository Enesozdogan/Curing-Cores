using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AITypePatrol : AIType
{

    public DirDetection dirDetection;

    private Vector2 movVec=Vector2.zero;
    private void Awake()
    {

        if(dirDetection==null) dirDetection = GetComponentInChildren<DirDetection>();
    }
    private void Start()
    {
        dirDetection.OnActChangeDir += HandleChangeDir;
        movVec=new Vector2(UnityEngine.Random.value>0.5f ? 1:-1,0);
    }

    private void HandleChangeDir()
    {
        movVec *= new Vector2(-1, 1);
    }

    public override void Perform(EnemyAIBase enemyBrain)
    {
        enemyBrain.MovVec= movVec;
        enemyBrain.ActivateMovement(movVec);
    }
}
