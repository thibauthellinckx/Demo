using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public bool isMovingForward;
    public Vector3 moveDir{get; private set;}
    public Vector3 moveRot{get; private set;}
    public bool shoot;
    public bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        isShooting = Input.GetKey(KeyCode.Mouse0);
        shoot = Input.GetKeyUp(KeyCode.Mouse0);
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveRot = new Vector3(0f,horizontal,0f);
        moveDir = new Vector3(0f,0f,vertical);
    }
}
