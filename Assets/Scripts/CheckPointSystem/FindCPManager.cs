using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCPManager : MonoBehaviour
{
    private CheckPointManager cpManager;
    private void Awake()
    {
       cpManager=FindObjectOfType<CheckPointManager>();
    }
    public void Spawn()
    {
        cpManager.Spawn(gameObject);
    }
    public void RestartCP()
    {
        cpManager.RestartAllCP();
        cpManager.Spawn(gameObject);
    }
}
