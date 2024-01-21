using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPointScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _targetGO;

    [field:SerializeField]
    private UnityEvent OnCPActivation { get; set; }

    private void Start()
    {
        OnCPActivation.AddListener(()=> GetComponentInParent<CheckPointManager>().UpdateCP(this));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this._targetGO= collision.gameObject;
            OnCPActivation?.Invoke();
           
        }
    }
    public void SpawnGO()
    {
        _targetGO.transform.position = transform.position;
    }
    public void SetTarget(GameObject target)
    {
        _targetGO = target;
        GetComponent<Collider2D>().enabled = false;
    }
    public void DeactivateCP()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    public void ResetCP()
    {
        _targetGO=null;
        GetComponent<Collider2D>().enabled = true;
    }
}
