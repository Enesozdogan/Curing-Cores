using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickUp : PickableBase
{
    [SerializeField]
    private int healAmount;
    protected override void PickUpAction(AgentScript agent)
    {
        var dmgAndHeal=agent.GetComponent<DmgAndHeal>();

        if(dmgAndHeal==null) return;

        dmgAndHeal.HealHp(healAmount);
    }
}
