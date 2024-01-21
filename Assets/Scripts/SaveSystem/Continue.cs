using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Continue : MonoBehaviour
{
    private Button btn;
    private LvlManager lvlManager;

    private void Awake()
    {
        lvlManager = FindObjectOfType<LvlManager>();
        btn = GetComponent<Button>();
      
    }
    private void Start()
    {
        int index = SaveAndLoadFuns.LoadLevelIndex();
        if(index <= 0)
        {
            btn.interactable = false;
        }
        btn.onClick.AddListener(() =>
        {   
            if ( index> 0)
            {
                lvlManager.GoToNextLvl(index);
                btn.interactable = true;
            }
            
        });
    }
}
