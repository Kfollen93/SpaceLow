using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    // Health bar
    private int maxHealth = 3;
    public int currentHealth;
    public HealthBar healthBar;

    // Handling player death 
    [SerializeField] private GameObject player;
    public GameObject deathObjectsOn;
    public bool isDead;

    public PlayerFlashRedOnHit playerFlashRed;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 1)
        {
            isDead = true;
            player.SetActive(false);
            deathObjectsOn.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        TakeDamageSound scriptDamageSound = FindObjectOfType(typeof(TakeDamageSound)) as TakeDamageSound;

        if (collision.gameObject.CompareTag("Sword") || collision.gameObject.CompareTag("EnemyProjectile"))
        {
            if (!isDead)
            {
                scriptDamageSound.TakeDmgSoundEffect();
                StartCoroutine(playerFlashRed.FlashRed());
                TakeDamage(1);
                healthBar.SetHealth(currentHealth);
            }
        }
    }
}
