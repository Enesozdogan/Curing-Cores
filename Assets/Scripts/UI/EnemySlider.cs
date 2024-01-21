using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlider : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    Transform agent;
    [SerializeField] Vector3 enemyOffset;


    private void Start()
    {
        slider.maxValue=agent.gameObject.GetComponent<AgentScript>().agentSO.health;
        //Debug.Log(slider.maxValue);
        slider.value = slider.maxValue;
    }
    void Update()
    {
        slider.transform.position=Camera.main.WorldToScreenPoint(agent.position+enemyOffset);
    }
}
