using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tank
{
public class Enemy : MonoBehaviour, IHealthbar
{
    [SerializeField]private float health = 100;
    public float Health{get{return health;} set{health = value;}}
    public event EnemyDeadDelegate OnEnemyDeath;


    void Start()
    {
        ScoreUI.Instance.AddEnemy(this.gameObject);
    }

    public void TakeDmg(float dmg)
    {
        Health -= dmg;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if(OnEnemyDeath != null)
        {
            OnEnemyDeath(this, new EventArgs());
            Debug.Log("event on dead called");
        }
        Destroy(gameObject);
    }
}
}
