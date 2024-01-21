using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnableCollision : MonoBehaviour
{
    public LayerMask colMask;
    public UnityEvent OnActColEnter, OnActColExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Bu kontrol 1 degerini binary olarak carpilan objenin degerine oteler ve colmask degeri ile karsilastirir
        if((1 << collision.gameObject.layer & colMask) != 0)
        {
            OnActColEnter?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & colMask) != 0)
        {
            OnActColExit?.Invoke();
        }
    }
}
