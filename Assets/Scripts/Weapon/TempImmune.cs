using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempImmune : MonoBehaviour, IToBeHit
{
    [SerializeField]
    private CapsuleCollider2D col;
    [SerializeField]
    private float immuneDur=0.5f;
    public void GetDamaged(GameObject go, int dmg)
    {
        if(this.enabled==false) return;
        StartCoroutine(GrantImmunity());
    }

    IEnumerator  GrantImmunity()
    {
        col.enabled = false;
        yield return new WaitForSeconds(immuneDur);
        col.enabled = true;
    }
}
