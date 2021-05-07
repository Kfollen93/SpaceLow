using UnityEngine;
using System;

public class EnemyKillCount : MonoBehaviour
{ 
    public static int  WaveAICount;
    private EnemyHealth destroyEnemy;

    private void Start()
    { 
        destroyEnemy = GetComponent<EnemyHealth>();
        destroyEnemy.OnEnemyKilled += EnemyHealth_OnEnemyKilled;
    }
    
    private void EnemyHealth_OnEnemyKilled(object sender, EventArgs e)
    { 
        WaveAICount++;

        // Unsubcribe from event method 
        destroyEnemy.OnEnemyKilled -= EnemyHealth_OnEnemyKilled; 
    }
}