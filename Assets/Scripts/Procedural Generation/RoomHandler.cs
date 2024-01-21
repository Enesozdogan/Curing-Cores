using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public roomType roomType;

    private void Awake()
    {
        if (FindObjectOfType<RoomGenerator>() == null)
        {
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject); 
    }
}
public enum roomType
{
    LR=0,
    LRB=1,
    LRU=2,
    LRUB=3
}
