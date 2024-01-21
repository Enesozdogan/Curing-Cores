using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundedFeedBack : MonoBehaviour
{
    public UnityEvent WannaPlaySound;
    [SerializeField]
    private GroundCheck gr;

    public void CallWannaPlay()
    {
        if(gr.isGrounded)
            WannaPlaySound?.Invoke();
    }
}
