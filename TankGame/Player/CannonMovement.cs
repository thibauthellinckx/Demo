using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    private float xRot = 65f;
    [SerializeField]public Transform shotPoint;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
        CalculateCannonAngle();
        
    }

    

    

    private void CalculateCannonAngle()
    {
        float mouseMove = Input.GetAxisRaw("Mouse Y");
        xRot -= mouseMove;
        xRot = Mathf.Clamp(xRot, 0, 130);
        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }

    
}
