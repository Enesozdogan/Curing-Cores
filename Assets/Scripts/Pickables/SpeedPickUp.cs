using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickUp : PickableBase
{
    [SerializeField]
    private float incAmount;
    protected override void PickUpAction(AgentScript agent)
    {
        var tuner=agent.GetComponent<PlayerSOTuner>();
        if (tuner == null) { return; }
        tuner.IncreasePlatforming(incAmount);
    }


}
