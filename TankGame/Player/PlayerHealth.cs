using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tank
{
public class PlayerHealth : MonoBehaviour,IHealthbar
{
    [SerializeField]private float health = 100;
    public float Health{get{return health;} set{health = value;}}

    public GameOverScreen gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        gameOverScreen.Setup(ScoreUI.Instance.score);
        gameObject.SetActive(false);
    }
}
    
}
