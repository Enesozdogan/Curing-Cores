using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreateMeleeWeapon", menuName = "CreateWeapon/Melee")]
public class MeleeBaseScript : WeaponInfo
{
    public float range;
    public override void UseAttack(AgentScript agentScript, LayerMask toBeHitMask, Vector3 dir)
    {

        
        RaycastHit2D[] hits = Physics2D.RaycastAll(agentScript.weaponTransform.position, dir, range, toBeHitMask);

        foreach (RaycastHit2D hit in hits)
        {
            IToBeHit[] enemyIToBeHits = hit.collider.GetComponents<IToBeHit>();
            if (enemyIToBeHits != null )
            {
                foreach(IToBeHit toBeHit in enemyIToBeHits)
                {
                    toBeHit.GetDamaged(agentScript.gameObject, damage);
                }
                
            }
        }
    }

    public override void WeaponGiz(Vector3 start, Vector3 dir)
    {
        Gizmos.DrawLine(start, start + range * dir);
    }
}
