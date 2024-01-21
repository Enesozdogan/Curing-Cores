using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cam;
    [SerializeField]
    private CheckPointManager cpManager;
    [SerializeField]
    private AgentScript playerAgent;
    [SerializeField]
    private LvlManager lvlManager;
 

    private void Awake()
    {
        cam=FindObjectOfType<CinemachineVirtualCamera>();
        cpManager=FindObjectOfType<CheckPointManager>();
        playerAgent=FindObjectOfType<PlayerInputController>().GetComponentInChildren<AgentScript>();
        lvlManager=FindObjectOfType<LvlManager>();
    }

    private void Start()
    {
       
        playerAgent.gameObject.SetActive(false);
        cpManager.Spawn(playerAgent.gameObject);
        Cursor.visible = false;
        SetCam();
        SystemLoad();

    }
    private void SetCam()
    {
        cam.LookAt = playerAgent.transform;
        cam.Follow=playerAgent.transform;
    }
    public void SaveLvl()
    {
        SaveAndLoadFuns.SaveLevelIndex(lvlManager.NextLvlIndex());
    }

    public void SystemSave()
    {
        IEnumerable<ILoadAndSave> saveImplants=FindObjectsOfType<MonoBehaviour>().OfType<ILoadAndSave>();
        foreach (ILoadAndSave data in  saveImplants)
        {
            data.Save();
        }
        
        SaveLvl();
    }
    public void SystemLoad()
    {
        IEnumerable<ILoadAndSave> saveImplants = FindObjectsOfType<MonoBehaviour>().OfType<ILoadAndSave>();
        foreach (ILoadAndSave data in saveImplants)
        {
            data.Load();
        }
    }
}
