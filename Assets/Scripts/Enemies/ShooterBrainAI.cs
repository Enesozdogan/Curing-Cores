using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBrainAI : EnemyAIBase
{
    [SerializeField]
    private AIType shootingType;

    private void Update()
    {
       shootingType.Perform(this);
    }
}
