using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    EnemyMovements enemyMovements;
    bool isTagetInRange;
    public GameObject currentGO;
    public LayerMask mask;
    [SerializeField]Vector3 origin;
    [SerializeField]Vector3 dir;
    [SerializeField]float currentHitDistance = 0;
    [SerializeField]private float sphereRadius = 7;
    [SerializeField]private float maxDistance;
    float dist;

    float cannonRot;
    bool isCannonRotSet;
    [SerializeField]private GameObject cannon;
    [SerializeField]private GameObject cannonBall;
    [SerializeField]private Transform shotPoint;

    float timer = 5f;

    
    void Start()
    {
        enemyMovements = GetComponent<EnemyMovements>();
    }

    
    void Update()
    {
        CheckIfEnemy();
        Timer();

        if (isTagetInRange)
        {
            RotateTowardsTarget();
            AdjustCannon();
        }

    }

    private void Timer()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 0;
        }
    }

    private void CheckIfEnemy()
    {
        origin = transform.position;
        dir = transform.forward;
        RaycastHit hit;

        if (Physics.SphereCast(origin, sphereRadius, transform.forward, out hit, maxDistance, mask))
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.gameObject.tag == "Player")
            {
            currentGO = hit.transform.gameObject;
            }
            isTagetInRange = true;
            enemyMovements.isMoving = false;
        }
        if (currentGO)
        {
            dist = Vector3.Distance(transform.position, currentGO.transform.position);
            if (dist > 10)
            {
                isTagetInRange = false;
                enemyMovements.isMoving = true;
            }
        }
    }

    private float SetRandomRotation()
    {
        cannonRot = UnityEngine.Random.Range(20,110);
        isCannonRotSet = true;
        return cannonRot;
    }

    private void AdjustCannon()
    {
        cannonRot = isCannonRotSet ? cannonRot : SetRandomRotation();
    
        var targetRot =  Quaternion.Euler(cannonRot,0,0);
        cannon.transform.localRotation = Quaternion.Lerp(cannon.transform.localRotation, targetRot, 5 * Time.deltaTime);
        if(cannon.transform.localRotation == targetRot && timer <= 0)
        {
            Shoot();
            isCannonRotSet = false;
            timer = 5;
        }
    }

    private void Shoot()
    {
        float power = UnityEngine.Random.Range(0,20);
        GameObject createdCannonBall = Instantiate(cannonBall, shotPoint.position, shotPoint.rotation);
        createdCannonBall.GetComponent<CannonBall>().shooterTag = gameObject.tag;

        createdCannonBall.GetComponent<Rigidbody>().velocity = shotPoint.transform.up * power;
    }

    private void RotateTowardsTarget()
    {
        float speed = 3f;
        dir = new Vector3(currentGO.transform.position.x , 0, currentGO.transform.position.z) - new Vector3(transform.position.x,0,transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation , speed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + dir * currentHitDistance);
        Gizmos.DrawWireSphere(origin + dir * currentHitDistance,sphereRadius);
    }
}

