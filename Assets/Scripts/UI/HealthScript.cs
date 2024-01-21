using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private float dmgSpeed = 0.3f,duration;
    int maxHp;
    private void Start()
    {
        InitializeHealth(70);
    
    }
    public void InitializeHealth(int maxHP)
    {
        healthSlider.value = maxHP;
        maxHp= maxHP;
    }
  
    public void UpdateHealth(int ToHp)
    {
        
        StartCoroutine(AnimateHealth2(ToHp));
        
    }
    IEnumerator AnimateHealth(int ToHp)
    {
        if(healthSlider.value < ToHp)
        {
           while(healthSlider.value <= ToHp)
            {
                healthSlider.value += Time.deltaTime * dmgSpeed;
                yield return null;  
            }
        }
        else
        {
            while (healthSlider.value >= ToHp)
            {
                healthSlider.value -= Time.deltaTime * dmgSpeed;
                yield return null;
            }
        }
        if (healthSlider.value < ToHp)
        {
            healthSlider.value+= ToHp - healthSlider.value;
        }
        else
        {
            healthSlider.value -=healthSlider.value- ToHp;
        }

    }
    IEnumerator AnimateHealth2(int  ToHp)
    {
        float time = 0;
        float startVal = healthSlider.value;

        while (time < duration)
        {
            float t = time / duration;
            healthSlider.value = Mathf.Lerp(startVal, ToHp, t);
            time += Time.deltaTime;
            yield return null;
        }

        healthSlider.value = ToHp;
    }


}
