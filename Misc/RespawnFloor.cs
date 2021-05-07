using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFloor : MonoBehaviour
{
    [SerializeField] private PlayerTakeDamage playerTakeDmgScript;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter(Collider respawnFloor)
    {
        if (respawnFloor.CompareTag("Player"))
        {
            PlayerIsDead();
        }
    }

    private void PlayerIsDead()
    {
        playerTakeDmgScript.isDead = true;
        playerTakeDmgScript.currentHealth = 0;
        playerTakeDmgScript.deathObjectsOn.SetActive(true);
        player.SetActive(false); 
    }
}
