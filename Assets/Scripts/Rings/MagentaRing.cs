using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagentaRing : RingBase
{
    public bool makeTrail = true;
    public bool canTp = true;
    [SerializeField]
    Slider slider;
    [SerializeField]
    private float teleportDur, teleportCd;
    public override bool PerformSkill()
    {
        if (canTp)
        {
            canTp= false;
            makeTrail = true;
            StartCoroutine(DurationTimer());
            StartCoroutine(CdTimer());  
            return true;
        }
       else return false;
    }

   
    public override void UpdateMatColor()
    {
        auraMat.SetColor("_Color", outlineColor);
    }
    
    IEnumerator DurationTimer()
    {
        yield return new WaitForSeconds(teleportDur);
        makeTrail= false;
    }
    IEnumerator CdTimer()
    {
        //yield return new WaitForSeconds(teleportCd);
        float elapsedTime = 0f;

        while (elapsedTime < teleportCd)
        {
            elapsedTime += Time.deltaTime;
            slider.value = elapsedTime / teleportCd;

            yield return null;
        }
        canTp = true;
    }


}
