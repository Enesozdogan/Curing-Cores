using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RangedWeapon", menuName = "CreateWeapon/Ranged")]
public class DataRanged : WeaponInfo
{
    public GameObject weaponPrefab;
    public int weaponSpeed;
    public int rangeVal;
    

    public override void UseAttack(AgentScript agentScript, LayerMask toBeHitMask, Vector3 dir)
    {
        GameObject weapon=Instantiate(weaponPrefab,agentScript.weaponTransform.position,Quaternion.identity);
        weapon.GetComponent<RangedWeapon>().SetWeaponData(dir, toBeHitMask, this);
    }
}
