using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHelperManager : MonoBehaviour
{
    
    public void StartGameSave()
    {
        SaveAndLoadFuns.DeleteSaves();
    }

}
