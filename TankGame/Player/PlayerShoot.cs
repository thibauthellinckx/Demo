using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]private PlayerInputs inputs;
    [SerializeField]public GameObject cannonBall;
    [SerializeField]public Transform shotPoint;
    [SerializeField]private float power = 10;
    [SerializeField]public float Power{get{return power;} set{power = Mathf.Clamp(value, 10,100);}}
    [SerializeField]private int ammo = 3;
    [SerializeField]public int Ammo{get{return ammo;}}
    private float scalingSpeed =2;
    [SerializeField]float ammoTimer=3f;

    float shootCooldown = 2f;
    bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        AmmoTimer();
        ShootTimer();

        if (inputs.shoot && canShoot)
        {
            fireCannonBall();
            canShoot = false;
            shootCooldown = 2;
            Power = 10f;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Power += Time.deltaTime * scalingSpeed;

        }
    }

    private void ShootTimer()
    {
        if(canShoot)return;
        shootCooldown -= Time.deltaTime;
        if(shootCooldown <= 0)
        {
            canShoot = true;
        }
    }
    private void fireCannonBall()
    {
        if(ammo <= 0)return;
        GameObject createdCannonBall = Instantiate(cannonBall, shotPoint.position, shotPoint.rotation);
        createdCannonBall.GetComponent<CannonBall>().shooterTag = gameObject.tag;
        createdCannonBall.GetComponent<Rigidbody>().velocity = shotPoint.transform.up * power;
        ammo--;
    }

    private void AmmoTimer()
    {
        if(ammoTimer > 0 && ammo < 3)
        {
            ammoTimer -= Time.deltaTime;
        }
        if(ammoTimer <= 0)
        {
            ammo++;
            ammoTimer = 3;
        }
    }

    
}
