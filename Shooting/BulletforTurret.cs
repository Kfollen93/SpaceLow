using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletforTurret : MonoBehaviour
{
    public float speed = 10f;
    public GameObject collisionExplosion;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * speed);
        Destroy(gameObject, 0.8f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject explosion = Instantiate(collisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 3f);
        }
    }
}
