using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrimsonRing : RingBase
{
    
    [SerializeField]
    private bool canSlash = true;
    [SerializeField]
    Slider slider;
    public override void OnEnter()
    {
        agent.weaponInfo.damage +=2;
    }
    public override void OnExit()
    {
        
        agent.weaponInfo.damage -= 2;
    }
    public override bool PerformSkill()
    {
        if (agent.groundCheck.isGrounded && canSlash)
        {
            canSlash = false;
            StartCoroutine(SlashCd());
            agent.ShiftToState(agent.state_factory.GetState(State_Factory.stateEnum.bloodSlash));
            return true;
        }
        return false;
       
    }

    IEnumerator SlashCd()
    {
        //yield return new WaitForSeconds(3);
        float elapsedTime = 0f;

        while (elapsedTime < 3)
        {
            elapsedTime += Time.deltaTime;
            slider.value = elapsedTime / 3;

            yield return null;
        }

        canSlash = true;
       
    }
    public override void UpdateMatColor()
    {
        auraMat.SetColor("_Color", outlineColor);
    }
  

}
