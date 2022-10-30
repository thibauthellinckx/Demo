using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IHealthbar
{
    float Health{get;}
    void TakeDmg(float dmg);
    void Die();
}

