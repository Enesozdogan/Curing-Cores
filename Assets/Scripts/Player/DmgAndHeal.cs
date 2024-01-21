using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DmgAndHeal : MonoBehaviour, IToBeHit
{
    [SerializeField]
    private int maxHp;
    [SerializeField]
    private int currHp;
  
    public int CurrHp
    {
        get { return currHp; }
        set
        {
            currHp = value;
            OnActHpChange?.Invoke(currHp);

        }
    }
    public UnityEvent OnActDie;
    public UnityEvent OnActGetDmg;
    public UnityEvent OnActGetHeal;
    public UnityEvent<int> OnActHpChange;
    public UnityEvent<int> OnActStartMaxHp;

    public void GetDamaged(GameObject go, int dmg)
    {
        GetDamaged(dmg);
        Debug.Log(go.name+ " Tarafindan Verilen   Hasar: " + dmg);
    }

    public void GetDamaged(int dmg)
    {
        CurrHp -= dmg;
        if (CurrHp <= 0)
        {
            OnActDie?.Invoke();
        }
        else
        {
            OnActGetDmg?.Invoke();
        }
    }
    public void HealHp(int amount)
    {
        CurrHp=Mathf.Clamp(currHp+amount,0, maxHp);
        OnActGetHeal?.Invoke();
    }
    public void StartHp(int amount)
    {
        maxHp = amount;
        OnActStartMaxHp?.Invoke(maxHp);
        CurrHp = maxHp;
    }
}
