using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public string shooterTag;
 
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == shooterTag)return;
        var healthbar = collisionInfo.gameObject.GetComponentInParent<IHealthbar>();
        healthbar?.TakeDmg(10);
        Destroy(gameObject);
    }
}
