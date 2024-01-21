using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingManager : MonoBehaviour
{
    public RingBase[] rings;

    public PlayerInputController inputController;


    public int index=1;

    public int oldIndex;

    
    private void Start()
    {
        inputController.OnActRingSwap += SwapRing;
       

    }



    public void SwapRing()
    {

        
        rings[index].OnExit();
        oldIndex = index;
        index++;
       
        if (index==rings.Length)
        {
            
            index = 0;
        }
        
        ApplyRingAttributes(index);

    }
    public void ApplyRingAttributes(int index)
    {
        rings[index].OnEnter();
        rings[index].UpdateMatColor();
    }
    public bool PerformSkill()
    {
        if (rings[index].PerformSkill()) return true;
        else  return false;
    }
    public enum ringTypes
    {
        crimson,
        azure,
        magenta
    }
}
