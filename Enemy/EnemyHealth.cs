using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    public int initialHealth = 8;
    private int currentHealth;
    public event EventHandler OnEnemyKilled; 

    private void Start()
    {
        currentHealth = initialHealth;
    }

    public void DestroyEnemy()
    { 
        Destroy(gameObject);
        OnEnemyKilled?.Invoke(this, EventArgs.Empty);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            DestroyEnemy(); 
        }
    }
}

/*  This needs a character controller to work.  Enemies will not take damage without one.
    Make the character controller circle BIGGER than the box collider.  The CC will be the one that connects the damage function
    the box collider will be for not running through the enemy.
    CC Collides against objects, ONLY in the direction it's currently moving. So if you have a moving enemy it needs a box collider too,
    but make sure box collider is smaller than the CC. */