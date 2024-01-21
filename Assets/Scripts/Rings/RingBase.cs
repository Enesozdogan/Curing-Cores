using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class RingBase : MonoBehaviour
{
    public Material auraMat;

    public Color outlineColor;
    

    public AgentScript agent;

   
    public abstract bool PerformSkill();

    public abstract void UpdateMatColor();

    public virtual void OnEnter()
    {

    }
    public virtual void OnExit()
    {

    }

}
