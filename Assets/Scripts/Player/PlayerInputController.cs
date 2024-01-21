using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputController : MonoBehaviour, IAgentController
{
    [field: SerializeField]
    public Vector2 MovVec { get; private set; }

    public event Action OnActAttack, OnActJumpPressed, OnActJumpReleased, OnActRingSwap, OnActPerformSkill,OnDialogAction;

    public event Action<Vector2> OnActMovement;
    public bool isInDialog = true;

    public KeyCode jumpKey, attackKey, menuKey, skillKey, swapKey,dialogKey;

    public UnityEvent OnMenuKeyPressed;
   
    private void Update()
    {
        if (Time.timeScale > 0)
        {
            GetMovementInput();
            GetJumpInput();
            GetAttackInput();
            GetRingSwapInput();
            GetSkillInput();
            GetDialogInput();

        }
       
        GetMenuInput();
    }

    private void GetRingSwapInput()
    {
        if (Input.GetKeyDown(swapKey))
        {
            OnActRingSwap?.Invoke();
        }
    }
    private void GetSkillInput()
    {
        if (Input.GetKeyDown(skillKey))
        {
            OnActPerformSkill?.Invoke();
        }
    }

    private void GetMenuInput()
    {
        if (Input.GetKeyDown(menuKey))
        {
            OnMenuKeyPressed?.Invoke();
        }
    }

    private void GetAttackInput()
    {
        if (Input.GetKeyDown(attackKey) && !isInDialog)
        {

            OnActAttack?.Invoke();
        }
    }
 

    private void GetJumpInput()
    {
        if (Input.GetKeyDown(jumpKey) )
        {
           
            OnActJumpPressed?.Invoke();
          
        }
    
        
        if (Input.GetKeyUp(jumpKey))
        {
            OnActJumpReleased?.Invoke();
        }


    }

    private void GetMovementInput()
    {
        MovVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnActMovement?.Invoke(MovVec);
    }

    private void GetDialogInput()
    {
        if (Input.GetKeyDown(dialogKey) && isInDialog)
        {
            OnDialogAction?.Invoke();
        }
    }


}
