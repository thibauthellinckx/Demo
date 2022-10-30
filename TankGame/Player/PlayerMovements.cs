using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    PlayerInputs inputs;
    [SerializeField]private float speed = 10f;
    [SerializeField]private float rotSpeed = 5;
    private Rigidbody rb;
    Vector3 direction;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputs = gameObject.GetComponent<PlayerInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = inputs.moveDir * speed * Time.deltaTime;
        direction = transform.TransformDirection(direction);
        
        transform.Rotate(inputs.moveRot * rotSpeed* Time.deltaTime);
    }
    void FixedUpdate()
    {
        MovePlayer(direction);
    }

    private void MovePlayer(Vector3 direction)
    {
        rb.MovePosition(transform.position + direction);
    }

}
