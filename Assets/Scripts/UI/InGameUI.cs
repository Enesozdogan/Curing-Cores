using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{

    private LvlManager lvlManager;
    [SerializeField]
    GameObject menuUI;

    private void Awake()
    {
        lvlManager = FindObjectOfType<LvlManager>(); 
    }

    public void RestartLvl()
    {
        lvlManager.RestartLvl();
    }
    public void LoadMenu()
    {
        lvlManager.LoadStartMenu();
    }
    public void ActivateCursor()
    {
        Cursor.visible = true;
    }
    public void DeactivateCursor()
    {
        Cursor.visible = false;
    }
    public void ActivateMenu()
    {


        if (menuUI.activeSelf)
        {
            menuUI.SetActive(false);
            Time.timeScale = 1;
            DeactivateCursor();
           
        }
        else
        {
            menuUI.SetActive(true);
            Time.timeScale = 0;
            ActivateCursor();
        }
     
    }
    public void MakeTimeZero()
    {
        Time.timeScale = 1;
    }
}
