using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random; 
using System;

public class ArenaHandler : MonoBehaviour
{
    // make this static or public if you need to reference this outside of this script 
    [SerializeField] private GameObject[] enemyArray; 
    public Transform[] spawnPoints;  // AI spawn points 
    [SerializeField] private float _spawnRate; // rate enemies will spawn 
    [SerializeField] private ArenaStart colTrigger; // trigger to start spawn system 
    [SerializeField] private bool _spawnRandomly;
    private bool _eventTriggered; 
    public static int enemiesLeft;
    public TMP_Text enemiesCounter;

    public void Start() 
    { 
        // event handler for when to start spawning enemies 
        colTrigger.OnPlayerEnters += ColTrigger_OnPlayerEnters;
        if (!_spawnRandomly && (enemyArray.Length != spawnPoints.Length)) 
        { 
            throw new Exception("length of enemy array and spawn points must be equal!");
        }
    }

    private void Update()
    { 
        if (_eventTriggered)
        {
            enemiesLeft = enemyArray.Length - EnemyKillCount.WaveAICount;
            enemiesCounter.text = $"Enemies Remaining: {enemiesLeft}";
        }

        if (_eventTriggered && enemiesLeft <= 0)
        {
            enemiesCounter.enabled = false;
        }
        
        // log example of how to keep track of enemies remaining 
        Debug.Log("Enemies remaining..." + enemiesLeft);
        if (_eventTriggered && EnemyKillCount.WaveAICount == enemyArray.Length)
        {
            Debug.Log("Robots destroyed");
            return;
        }
    }

    private void ColTrigger_OnPlayerEnters(object sender, EventArgs e) 
    {
        StartBattle(); 
    }
    
    private void StartBattle() 
    {
        StartCoroutine(SpawnEnemy());
        _eventTriggered = true;
    }

    public IEnumerator SpawnEnemy() 
    { 
        if (_spawnRandomly)
        { 
            foreach (GameObject enemy in enemyArray)
            { 
                Transform _sp = spawnPoints[ Random.Range(0,spawnPoints.Length)]; // randomly select where spawn points will be 
                Instantiate(enemy, _sp.position, _sp.rotation);
                yield return new WaitForSeconds(_spawnRate);
            }
        }
        else 
        { 
            int i = 0;
            // check that length array == length spawn points 
            foreach (GameObject enemy in enemyArray) 
            { 
                Transform _sp = spawnPoints[i];
                Instantiate(enemy, _sp.position, _sp.rotation); 
                yield return new WaitForSeconds(_spawnRate); 
                i++; 
            }
        }
    }
}