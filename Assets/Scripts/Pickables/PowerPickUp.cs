using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickUp : PickableBase
{
    [SerializeField]
    private int incAmount;
    protected override void PickUpAction(AgentScript agent)
    {
        var tuner=agent.GetComponent<PlayerSOTuner>();
        if (tuner == null) { return; }
        tuner.IncreasePower(incAmount);
    }


}
