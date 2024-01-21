using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex3D : MonoBehaviour
{
    [SerializeField]
    private float movSpeed = 0.2f;

    [SerializeField]
    private Camera mainCam;

    private void Awake()
    {
        if (mainCam == null) mainCam = Camera.main;
    }
    private void FixedUpdate()
    {
        transform.position = new Vector2(movSpeed * mainCam.transform.position.x, transform.position.y);
    }
}
