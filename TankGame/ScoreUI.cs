using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

namespace Tank
{
public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreUI;
    public int score;
    public static ScoreUI Instance { get; private set; }
    [SerializeField]private List<GameObject> EnemyList;
    private void Awake() 
    { 
        EnemyList = new List<GameObject>();
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void Start()
    {
    }

    public void UpdateUI(object sender, EventArgs args)
    {
        score++;
        scoreUI.text  = score.ToString();
        Debug.Log("ui updated");
    }

    public void AddEnemy(GameObject enemy)
    {
        EnemyList.Add(enemy);
        enemy.GetComponent<Enemy>().OnEnemyDeath += UpdateUI;
    }
}
}
