using UnityEngine;

public class SpawnExit : MonoBehaviour { 

    [SerializeField] private GameObject exitPath;

    public void Update() 
    { 
        if (ArenaHandler.enemiesLeft == 0 && EnemyKillCount.WaveAICount > 0)
        { 
            exitPath.SetActive(true); 
        }
    }
}