// EnemyProjectile.cs 
/** This will handle the different kinds of projectiles 

How it should work: 
 - booleans are create fcor each projectile type. need to ensure object can't be assigned more than 1
 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float speed;
    private Transform player;
    private Vector3 target;
    public GameObject collisionExplosion;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y + 3, player.position.z);
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            DestroyProjectile();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && target != null)
        {
            GameObject explosion = Instantiate(collisionExplosion, transform.position, transform.rotation);
            Destroy(explosion, 3f);
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject, 3f);
    }
}
