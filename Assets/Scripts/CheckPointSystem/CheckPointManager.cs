using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    List<CheckPointScript> checkPoints= new List<CheckPointScript>();
    CheckPointScript currCP;

    private void Awake()
    {
        foreach(Transform item in transform)
        {
            checkPoints.Add(item.GetComponent<CheckPointScript>());
        }
        currCP = checkPoints[0];
    }
    public void UpdateCP(CheckPointScript newCP)
    {
        currCP.DeactivateCP();
        currCP = newCP;
    }
    public void Spawn(GameObject player)
    {
        currCP.SetTarget(player);
        currCP.SpawnGO();
        player.SetActive(true);
    }
    public void SpawnPlace(CheckPointScript cp,GameObject player)
    {
        cp.SetTarget(player);
        Spawn(player);
    }
    public void RestartAllCP()
    {
        foreach (var cp in checkPoints)
        {
            cp.ResetCP();
        }
        currCP = checkPoints[0];
    }
    
}
