using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponInfo : ScriptableObject
{
    public string weaponName;
    public int damage;
    public AudioClip soundFx;

    public abstract void UseAttack(AgentScript agentScript, LayerMask toBeHitMask, Vector3 dir);
    public virtual void WeaponGiz(Vector3 start, Vector3 dir)
    {

    }

}
