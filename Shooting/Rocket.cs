using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 20.0f;
    public float timeBeforeDespawn = 5.0f;
    public int damageDealt = 3;
    public GameObject collisionExplosion;

    private void Start()
    {
        // Invoke calls the method named in its first param after the provided duration.
        Invoke(nameof(Kill), timeBeforeDespawn);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider col) // on Trigger is checked on the rocket, this is so it triggers the health damage
    {
        EnemyHealth health = col.gameObject.GetComponent<EnemyHealth>();

        if (health != null)
        {
            health.TakeDamage(damageDealt);
        }

        Kill();
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    // Explosion Animation
    private void OnCollisionEnter(Collision collision) // This requires trigger to be off, so it collides for explosion to play.
    {
        GameObject explosion = Instantiate(collisionExplosion, transform.position, transform.rotation);
        Destroy(explosion, 3f);
        Destroy(gameObject);
    }
}
