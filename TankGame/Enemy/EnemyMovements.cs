using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    private Rigidbody rb;

    private float speed = 1f;
    
    private bool isRotating;
    private float rotateTimer = 3f;
    private float rotSpeed = 30;
    private float timeRotating=0;
    
    private Vector3 dir;
    private bool isMovingForward;
    public bool isMoving;
    public bool isTimerPaused;
    [SerializeField]private LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timeRotating = UnityEngine.Random.Range(1,5);
        rotSpeed = UnityEngine.Random.Range(-30,30);
        isMovingForward = true;
        isMoving = true;
        isTimerPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRotating && timeRotating > 0)
        {
            Rotate();
        }
        else
        {
            isRotating = false;
        };
        CollisionCheck();
        if(!isTimerPaused)RotateCountDown();
        CalculateDir();
    }

    private void CollisionCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2,layerMask))
        {
            isTimerPaused = true;
            isMoving = false;
            Rotate();
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }else
        {
        isMoving = true;
        isMovingForward = true;
        isTimerPaused = false;
        }
    }

    private void CalculateDir()
    {
        dir = Vector3.forward;
        dir = transform.TransformDirection(dir);
    }

    void FixedUpdate()
    {
        if(isMovingForward && isMoving)
        {
        
        Move(dir);
        } else if(!isMovingForward && isMoving)
        {
            Move(-dir);
        }
    }

    private void Rotate()
    {
        
        transform.Rotate(Vector3.up * rotSpeed* Time.deltaTime);
        timeRotating -= Time.deltaTime;
    }

    private void RotateCountDown()
    {
        rotateTimer -= Time.deltaTime;
        if(rotateTimer <= 0)
        {
            isRotating = true;
            ResetTimerForNextRotation();
            SetRotationSpeed();
            SetTimeRotating();
        }
    }

    private void ResetTimerForNextRotation()
    {
        rotateTimer = 10f;
    }

    private void SetTimeRotating()
    {
        timeRotating = UnityEngine.Random.Range(3,5);
    }

    private void SetRotationSpeed()
    {
        rotSpeed = UnityEngine.Random.Range(-30,30);
    }

    private void Move(Vector3 dir)
    {
        rb.MovePosition(transform.position + (dir * speed * Time.deltaTime));
    }
}
