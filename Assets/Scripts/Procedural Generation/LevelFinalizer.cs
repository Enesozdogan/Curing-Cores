using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LevelFinalizer : MonoBehaviour
{
    public RoomGenerator roomGenerator;
    void Start()
    {
        if (!roomGenerator.isGenerating)
        {
            GameObject[] childs = GetComponentsInChildren<GameObject>();
            
            foreach (var elemnt in childs)
            {
                Destroy(elemnt.GetComponent<ObjectSpawner>());
            }
            
        }
        
    }

}
