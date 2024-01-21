using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    private Vector2 originPos;
    private DataRanged dataRanged;
    
    private Vector2 movVecDir;
    private Rigidbody2D weaponRb2d;
    public bool isSet=false;
    [Header("Debug and Col")]
    [SerializeField]
    private Vector2 offset;
    [SerializeField]
    private float rad;
    [SerializeField]
    private Color gizCol = Color.green;
    [SerializeField]
    private LayerMask playerMask;

    private void Awake()
    {
        originPos = transform.position;
        weaponRb2d= GetComponent<Rigidbody2D>();
    }
    public void SetWeaponData(Vector2 dir,LayerMask plMask,DataRanged dataRanged)
    {
        isSet= true;
        this.movVecDir= dir;
        this.playerMask= plMask;
        this.dataRanged= dataRanged;
        weaponRb2d.velocity = movVecDir * dataRanged.weaponSpeed;
    }
    private void Update()
    {
        if (isSet)
        {
            CastCircle();
            DestroyOverRange();
        }
    }

    private void DestroyOverRange()
    {
        Vector2 rangeCal=(Vector2)transform.position- originPos;
        if (rangeCal.magnitude > dataRanged.rangeVal)
            Destroy(this.gameObject);
    }

    private void CastCircle()
    {
        var col = Physics2D.OverlapCircle((Vector2)transform.position + offset, rad, playerMask);
        if (col != null)
        {
            foreach(IToBeHit toBeHit in col.GetComponents<IToBeHit>())
            {
                toBeHit.GetDamaged(this.gameObject, dataRanged.damage);
            }
            Destroy(this.gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = gizCol;
        Gizmos.DrawSphere((Vector2)transform.position + offset, rad);
    }
}
